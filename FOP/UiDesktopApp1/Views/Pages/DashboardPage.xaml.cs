using System.Collections.ObjectModel;
using System;
using System.Security.Policy;
using UiDesktopApp1.ViewModels.Pages;
using Wpf.Ui.Controls;
using Helpers = UiDesktopApp1.Helpers;
using Microsoft.Win32;
using FOP.Core.Entities.Interfaces;
using FOP.Core.Services;
using FOP.Core.Models.Utils;
using MaterialDesignThemes.Wpf;
using FOP.Core.Models;
namespace UiDesktopApp1.Views.Pages
{
    public class Step
    {
        public int StepId { get; set; }
        public string Description { get; set; }
    }

    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }
        DynamicLimitsModel DynamicLimitsModel = new DynamicLimitsModel();
        //bindings
        int SelectedYear = DateTime.Now.Year;
        List<string> SelectedFileNames = new List<string>();
        List<string> SelectedFolder = new List<string>();

        Guid UploadID = new Guid();

        //initiate services
        private readonly IFileUploadService _fileUploadService;
        private readonly IMonthlyUploadService _monthlyUploadService;
        private readonly IAllMasterService _allMasterService;

        public DashboardPage(DashboardViewModel viewModel, IFileUploadService fileUploadService, IMonthlyUploadService monthlyUploadService, IAllMasterService allMasterService)
        {
            //setupservices
            _fileUploadService = fileUploadService;
            _monthlyUploadService = monthlyUploadService;
            _allMasterService = allMasterService;

            //model binds
            ViewModel = viewModel;
            DataContext = new DashboardViewModel();
            InitializeComponent();
            _ = updateAllFileStatus(1);
            InitialLoad();

            DynamicLimitsModel = new DynamicLimitsModel
            {
                MaxFlowPercentage = 10,
                MinFlowPercentage = 10,
                CashUMAPercentage = 10,
                CashOverPercentage = 5,
                CashOverDestPercentage = 4,
                EntityTestValue = 3000
            };

            maxflow.Text = DynamicLimitsModel.MaxFlowPercentage.ToString();
            minflow.Text = DynamicLimitsModel.MinFlowPercentage.ToString();
            cashUMA.Text = DynamicLimitsModel.CashUMAPercentage.ToString();
            cashOver.Text = DynamicLimitsModel.CashOverPercentage.ToString();
            cashOverDest.Text = DynamicLimitsModel.CashOverDestPercentage.ToString();
            testValue.Text = DynamicLimitsModel.EntityTestValue.ToString();

            //after preps
            FileUpload.IsEnabled = false;
            ddYears.ItemsSource = Helpers.Utils.GetYearList();
            ddMonths.ItemsSource = Helpers.Utils.GetMonths();
            dd_yearCreateMaster.ItemsSource = Helpers.Utils.GetYearList();
            dd_monthCreateMaster.ItemsSource = Helpers.Utils.GetMonths();

            stPickedFiles.Visibility = SelectedFileNames.Count == 0 ? Visibility.Hidden : Visibility.Visible;

            //hide loaders at initial
            loader_grid.Visibility = Visibility.Hidden;
            OpenFileButton.Visibility = Visibility.Hidden;
        }
        private void InitialLoad()
        {
            VisibilityDownloadSection(true);
        }

        private void VisibilityDownloadSection(bool isHidden)
        {

            Download.Visibility = isHidden?Visibility.Hidden:Visibility.Visible;
            DownloadName.Visibility = isHidden ? Visibility.Hidden : Visibility.Visible;
            GridFolderPick.Visibility = isHidden ? Visibility.Hidden : Visibility.Visible;
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedYear = (int)ddYears.SelectedItem;
        }

        private void Button_OpenFiles(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "All files (*.*)|*.*",
            };

            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }

            if (openFileDialog.FileNames.Length == 0)
            {
                return;
            }
            else
            {
                stPickedFiles.Visibility = Visibility.Visible;
                FileUpload.IsEnabled = true;
                loader_grid.Visibility = Visibility.Visible;
            }

            SelectedFileNames = openFileDialog.FileNames.ToList();
            lbFileList.ItemsSource = SelectedFileNames;
        }
         
        private async void FileUpload_Click(object sender, RoutedEventArgs e)
        {
            FileUpload.IsEnabled = false;

            // Reorder SelectedFileNames according to FileType order
            SelectedFileNames = SelectedFileNames.OrderBy(file =>
            {
                if (file.Contains(FileTypes.SleeveStratergies)) return 0;
                if (file.Contains(FileTypes.GrossPerformanceBatch)) return 1;
                if (file.Contains(FileTypes.NetPerformanceBatch)) return 2;
                if (file.Contains(FileTypes.PortfolioAuditAccs)) return 3;
                if (file.Contains(FileTypes.PortfolioAuditRegs)) return 4;
                if (file.Contains(FileTypes.SleeveRegContributions)) return 5;
                if (file.Contains(FileTypes.CashQuery)) return 6;
                if (file.Contains(FileTypes.ModelChanges)) return 7;
                if (file.Contains(FileTypes.TerminatedAccounts)) return 8;
                return 9; // Default order for any other files
            }).ToList();

            int selectedMonth = DateTime.ParseExact(ddMonths.SelectedItem.ToString(), "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;
            int selectedYear = (int)ddYears.SelectedItem;
            var upload = await _monthlyUploadService.SaveDate(selectedMonth, selectedYear);  

            bool allFilesUploaded = true;

            foreach (var item in SelectedFileNames)
            {
                var res = await _fileUploadService.UploadFiles(item, upload);
                if (res.IsSuccess)
                {
                     updateFileUploadStatus(item, 2);
                }
                else
                {
                     updateFileUploadStatus(item, 3);
                    allFilesUploaded = false;
                }
            }

            if (allFilesUploaded)
            {
                var allMasterDetails = await _allMasterService.UploadAllMaster(selectedYear, selectedMonth);
                if (allMasterDetails.IsSuccess)
                {
                    updateFileUploadStatus("All Master", 2);
                    System.Windows.MessageBox.Show("Successfully uploaded", "Success", System.Windows.MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    updateFileUploadStatus("All Master", 3);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Error uploading some files", "Error", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
            }

            FileUpload.IsEnabled = true;
        }
          
        private async void updateFileUploadStatus(string file, int type = 1)
        {
            //default
            var progressbar = true;
            var done = false;
            var cancel = false;

            if (type == 1)
            {
                //progressbar
                progressbar = true;
                done = false;
                cancel = false;
            }
            else if (type == 2)
            {
                //done
                progressbar = false;
                done = true;
                cancel = false;
            }
            else if (type == 3)
            {
                //cancel
                progressbar = false;
                done = false;
                cancel = true;
            }

            if (file.Contains(FileTypes.GrossPerformanceBatch))
            {
                Gross_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
                Gross_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
                Gross_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
            }
            else if (file.Contains(FileTypes.NetPerformanceBatch))
            {
                Net_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
                Net_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
                Net_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
            }
            else if (file.Contains(FileTypes.PortfolioAuditAccs))
            {
                PAA_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
                PAA_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
                PAA_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
            }
            else if (file.Contains(FileTypes.PortfolioAuditRegs))
            {
                PAR_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
                PAR_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
                PAR_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
            }
            else if (file.Contains(FileTypes.SleeveRegContributions))
            {
                SRC_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
                SRC_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
                SRC_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
            }
            else if (file.Contains(FileTypes.CashQuery))
            {
                Cash_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
                Cash_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
                Cash_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
            }
            else if (file.Contains(FileTypes.ModelChanges))
            {
                MC_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
                MC_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
                MC_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
            }
            else if (file.Contains(FileTypes.TerminatedAccounts))
            {
                TA_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
                TA_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
                TA_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
            }
            else if (file.Contains(FileTypes.SleeveStratergies))
            {
                Sleeve_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
                Sleeve_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
                Sleeve_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
            }
            else if (file.Contains(FileTypes.AllMaster))
            {
                AM_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
                AM_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
                AM_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private async Task updateAllFileStatus(int type = 1)
        {
            //default
            var progressbar = true;
            var done = false;
            var cancel = false;


            if (type == 1)
            {
                //progressbar
                progressbar = true;
                done = false;
                cancel = false;
            }
            else if (type == 2)
            {
                //done
                progressbar = false;
                done = true;
                cancel = false;
            }
            else if (type == 3)
            {
                //cancel
                progressbar = false;
                done = false;
                cancel = true;
            }

            Gross_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
            Gross_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
            Gross_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;

            Net_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
            Net_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
            Net_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;

            PAA_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
            PAA_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
            PAA_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;

            PAR_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
            PAR_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
            PAR_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;

            SRC_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
            SRC_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
            SRC_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;

            Cash_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
            Cash_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
            Cash_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;

            MC_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
            MC_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
            MC_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;

            TA_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
            TA_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
            TA_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;

            Sleeve_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
            Sleeve_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
            Sleeve_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;

            AM_PB.Visibility = progressbar ? Visibility.Visible : Visibility.Hidden;
            AM_Done.Visibility = done ? Visibility.Visible : Visibility.Hidden;
            AM_Cancel.Visibility = cancel ? Visibility.Visible : Visibility.Hidden;
        }

        #region Text Changed Events

        private void minflow_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (decimal.TryParse(minflow.Text, out decimal value))
            {
                DynamicLimitsModel.MinFlowPercentage = value;
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (decimal.TryParse(maxflow.Text, out decimal value))
            {
                DynamicLimitsModel.MaxFlowPercentage = value;
            }
        }

        private void cashUMA_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (decimal.TryParse(cashUMA.Text, out decimal value))
            {
                DynamicLimitsModel.CashUMAPercentage = value;
            }
        }

        private void cashOver_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (decimal.TryParse(cashOver.Text, out decimal value))
            {
                DynamicLimitsModel.CashOverPercentage = value;
            }
        }

        private void cashOverDest_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (decimal.TryParse(cashOverDest.Text, out decimal value))
            {
                DynamicLimitsModel.CashOverDestPercentage = value;
            }
        }

        private void testValue_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (int.TryParse(testValue.Text, out int value))
            {
                DynamicLimitsModel.EntityTestValue = value;
            }
        }

        #endregion


        #region create master logic
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CM_Button.IsEnabled = false;
            int selectedMonth = DateTime.ParseExact(dd_monthCreateMaster.SelectedItem.ToString(), "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;
            int selectedYear = (int)dd_yearCreateMaster.SelectedItem;
            var MasterDetails = await _allMasterService.CreateMaster(selectedYear, selectedMonth, DynamicLimitsModel);
            if (MasterDetails.IsSuccess)
            {
                VisibilityDownloadSection(false);
                System.Windows.MessageBox.Show("Successfully created master", "Success", System.Windows.MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("Error creating master", "Error", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
            }
            CM_Button.IsEnabled = true;
        }
        #endregion

        private void OnOpenFolder(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new()
            {
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            if (openFolderDialog.ShowDialog() != true)
            {
                return;
            }

            if (openFolderDialog.FolderNames.Length == 0)
            {
                return;
            }
            SelectedFolder = openFolderDialog.FolderNames.ToList();
            lbFolderList.Text = SelectedFolder.FirstOrDefault();
        }



        private async void ShowInfoBar(InfoBarSeverity infoBarSeverity, string title, string msg)
        {
            CM_TitleInfoBar.IsOpen = true;
            CM_TitleInfoBar.Severity = infoBarSeverity;
            CM_TitleInfoBar.Title = title;
            CM_TitleInfoBar.Message = msg;
            await Task.Delay(3000); // Add a delay of 3 seconds
            CM_TitleInfoBar.IsOpen = false;
        }
        private async void Download_Click(object sender, RoutedEventArgs e)
        {
            int selectedMonth = DateTime.ParseExact(dd_monthCreateMaster.SelectedItem.ToString(), "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;
            int selectedYear = (int)dd_yearCreateMaster.SelectedItem;
            var uploadId = await _monthlyUploadService.GetUploadIdByMonthAndYear(selectedMonth, selectedYear);
            if (SelectedFolder.Count > 0 && uploadId != Guid.Empty)
            {
                foreach (DownloadFiles fileType in Enum.GetValues(typeof(DownloadFiles)))
                {
                    var result = await _allMasterService.DownloadFiles(fileType, uploadId, SelectedFolder.FirstOrDefault());
                    if (result.IsSuccess)
                    {
                        ShowInfoBar(InfoBarSeverity.Success, "Download", $"{fileType} Downloaded Successfully");
                    }
                    else
                    {
                        ShowInfoBar(InfoBarSeverity.Error, "Download", $"Error downloading {fileType}: {result.msg}");
                    }
                }
            }
        }

        private async void ddMonths_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ddMonths?.SelectedItem != null)
            {
                int selectedMonth = DateTime.ParseExact(ddMonths.SelectedItem.ToString(), "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;
                int selectedYear = (int)ddYears.SelectedItem;

                if (selectedMonth > 0)
                {
                    dd_yearCreateMaster.SelectedItem = ddYears.SelectedItem;
                    dd_monthCreateMaster.SelectedItem = ddMonths.SelectedItem;
                }

                UploadID = await _monthlyUploadService.GetUploadIdByMonthAndYear(selectedMonth, selectedYear);
                if (selectedMonth > 0 && UploadID == Guid.Empty)
                { 
                    OpenFileButton.Visibility = Visibility.Visible;
                }
                else
                {
                    OpenFileButton.Visibility = Visibility.Hidden;
                }
            }
        }

        private async void dd_monthCreateMaster_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
           // if (UploadID != Guid.Empty) {
                VisibilityDownloadSection(false);
           // }
        }
    }
}
