using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UiDesktopApp1.ViewModels.Pages;
using UiDesktopApp1.Views.Pages;

namespace UiDesktopApp1.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _counter = 0;
        private ObservableCollection<Step> _stepList;

        public ObservableCollection<Step> Steps
        {
            get { return _stepList; }
            set
            {
                _stepList = value;
                //  OnPropertyChanged(nameof(Step));
            }
        }

        public DashboardViewModel()
        {
            Steps = new ObservableCollection<Step>
               {
                   new Step { StepId = 1, Description = "John Doe" },
                   new Step { StepId = 2, Description = "Jane Smith" },
                   new Step { StepId = 2, Description = "Jane Smith" }
               };
        }


        [RelayCommand]
        private void OnCounterIncrement()
        {
            Counter++;
        }

    }



    public class Step
    {
        public int StepId { get; set; }
        public string Description { get; set; }
    }
}

