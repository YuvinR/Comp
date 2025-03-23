using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using ExcelDataReader;
using FOP.Core.Entities;
using FOP.Core.Entities.Interfaces;
using FOP.Core.Entities.Interfaces.Repository;
using FOP.Core.Models;
using FOP.Core.Models.Utils;

namespace FOP.Core.Services
{
    public class AllMasterService : IAllMasterService

    {
        private readonly IAllMasterRepository _allMasterRepository;
        private readonly IMonthlyUploadRepository _monthlyUploadRepository;
        public AllMasterService(IAllMasterRepository allMasterRepository, IMonthlyUploadRepository monthlyUploadRepository)
        {
            _allMasterRepository = allMasterRepository;
            _monthlyUploadRepository = monthlyUploadRepository;
        }

        public async Task<(bool IsSuccess, string msg)> UploadAllMaster(int year, int month)
        {
            try
            {
                var monthlyUpload = await _allMasterRepository.CreateAllMaster(year, month);


                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
                // Log exception
            }
        }

        public async Task<(bool IsSuccess, string msg)> CreateMaster(int year, int month, DynamicLimitsModel dynamicLimitsModel)
        {
            try
            {
                var monthlyUpload = await _allMasterRepository.CreateMaster(year, month, dynamicLimitsModel);
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string msg)> DownloadFiles(DownloadFiles downloadFileType, Guid uploadId, string foldername)
        {
            switch (downloadFileType)
            {
                case Models.Utils.DownloadFiles.AllMaster:
                    var resultAllMaster = await _allMasterRepository.GetDownloadContent(downloadFileType, uploadId);
                    return await ProcessAllMaster(resultAllMaster, foldername);

                case Models.Utils.DownloadFiles.Master:
                    var resultMaster = await _allMasterRepository.GetDownloadContent(downloadFileType, uploadId);
                    return await ProcessMaster(resultMaster, foldername);

                case Models.Utils.DownloadFiles.Orion:
                    var result = await _allMasterRepository.GetDownloadContent(downloadFileType, uploadId);
                    return await ProcessOrion(result, foldername);

                case Models.Utils.DownloadFiles.NoComposites:
                    var resultNoComposite = await _allMasterRepository.GetDownloadContent(downloadFileType, uploadId);
                    return await ProcessNoComposite(resultNoComposite, foldername);

                case Models.Utils.DownloadFiles.Exclusions:
                    var resultExclusions = await _allMasterRepository.GetDownloadContent(downloadFileType, uploadId);
                    return await ProcessExclusions(resultExclusions, foldername);
      
                default:
                    return (false, "Invalid download file type");
            }


        }

        public async Task<(bool IsSuccess, string msg)> ProcessOrion(List<AllMasterModel> data, string foldername)
        {
            try
            {
                var _dataTable = new System.Data.DataTable();

                _dataTable.Columns.Add("Entity ID");
                _dataTable.Columns.Add("Account Number");
                _dataTable.Columns.Add("Start Date");
                _dataTable.Columns.Add("Sleeve Strategy");
                _dataTable.Columns.Add("Business Line");
                _dataTable.Columns.Add("Period Beginning Market Value");
                _dataTable.Columns.Add("Period Ending Market Value");
                _dataTable.Columns.Add("Period Performance Gross");
                _dataTable.Columns.Add("Period Performance Net");
                _dataTable.Columns.Add("Entity Path");
                _dataTable.Columns.Add("IsUMA");
                _dataTable.Columns.Add("Legacy CLS AID");
                _dataTable.Columns.Add("Net Flow");
                _dataTable.Columns.Add("Flow %");
                _dataTable.Columns.Add("Cash %");
                _dataTable.Columns.Add("Terminated");
                _dataTable.Columns.Add("Model Change");
                _dataTable.Columns.Add("Entity Name");
                _dataTable.Columns.Add("Group Name");
                _dataTable.Columns.Add("Benchmark");

                foreach (var item in data)
                {
                    var row = _dataTable.NewRow();
                    row["Entity ID"] = item.EntityID;
                    row["Account Number"] = item.AccountNumber;
                    row["Start Date"] = item.StartDate;
                    row["Sleeve Strategy"] = item.SleeveStrategy;
                    row["Business Line"] = item.BusinessLine;
                    row["Period Beginning Market Value"] = Convert.ToDouble(item.PeriodBeginningMarketValue?.ToString("G29"));
                    row["Period Ending Market Value"] = Convert.ToDouble(item.PeriodEndingMarketValue?.ToString("G29"));
                    row["Period Performance Gross"] = item.PeriodPerformanceGross;
                    row["Period Performance Net"] = item.PeriodPerformanceNet;
                    row["Entity Path"] = item.EntityPath;
                    row["IsUMA"] = item.IsUMA;
                    row["Legacy CLS AID"] = item.LegacyCLSAID;
                    row["Net Flow"] = Convert.ToDouble(item.NetFlow?.ToString("G29"));
                    row["Flow %"] = Convert.ToDouble(item.FlowPercentage?.ToString("G29"));
                    row["Cash %"] = Convert.ToDouble(item.CashPercentage?.ToString("G29"));
                    row["Terminated"] = item.Terminated;
                    row["Model Change"] = item.ModelChange;
                    row["Entity Name"] = item.EntityName;
                    row["Group Name"] = item.GroupName;
                    row["Benchmark"] = item.Benchmark;
                    _dataTable.Rows.Add(row);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(_dataTable, "Orion");

                    wb.SaveAs(foldername + "\\Orion.xlsx");

                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            return (true, "Orion Sheet Downloaded!");
        }

        public async Task<(bool IsSuccess, string msg)> ProcessAllMaster(List<AllMasterModel> data, string foldername)
        {
            try
            {
                var _dataTable = new System.Data.DataTable();

                _dataTable.Columns.Add("Entity ID");
                _dataTable.Columns.Add("Account Number");
                _dataTable.Columns.Add("Start Date");
                _dataTable.Columns.Add("Sleeve Strategy");
                _dataTable.Columns.Add("Business Line");
                _dataTable.Columns.Add("Period Beginning Market Value");
                _dataTable.Columns.Add("Period Ending Market Value");
                _dataTable.Columns.Add("Period Performance Gross");
                _dataTable.Columns.Add("Period Performance Net");
                _dataTable.Columns.Add("Entity Path");
                _dataTable.Columns.Add("IsUMA");
                _dataTable.Columns.Add("Legacy CLS AID");
                _dataTable.Columns.Add("Net Flow");
                _dataTable.Columns.Add("Flow %");
                _dataTable.Columns.Add("Cash %");
                _dataTable.Columns.Add("Terminated");
                _dataTable.Columns.Add("Model Change");
                _dataTable.Columns.Add("Entity Name");
                _dataTable.Columns.Add("Group Name");
                _dataTable.Columns.Add("Benchmark");

                foreach (var item in data)
                {
                    var row = _dataTable.NewRow();
                    row["Entity ID"] = item.EntityID;
                    row["Account Number"] = item.AccountNumber;
                    row["Start Date"] = item.StartDate;
                    row["Sleeve Strategy"] = item.SleeveStrategy;
                    row["Business Line"] = item.BusinessLine;
                    row["Period Beginning Market Value"] = Convert.ToDouble(item.PeriodBeginningMarketValue?.ToString("G29"));
                    row["Period Ending Market Value"] = Convert.ToDouble(item.PeriodEndingMarketValue?.ToString("G29")); 
                    row["Period Performance Gross"] = item.PeriodPerformanceGross;
                    row["Period Performance Net"] = item.PeriodPerformanceNet;
                    row["Entity Path"] = item.EntityPath;
                    row["IsUMA"] = item.IsUMA;
                    row["Legacy CLS AID"] = item.LegacyCLSAID;
                    row["Net Flow"] = Convert.ToDouble(item.NetFlow?.ToString("G29")); 
                    row["Flow %"] = Convert.ToDouble(item.FlowPercentage?.ToString("G29"));
                    row["Cash %"] = Convert.ToDouble(item.CashPercentage?.ToString("G29"));
                    row["Terminated"] = item.Terminated;
                    row["Model Change"] = item.ModelChange;
                    row["Entity Name"] = item.EntityName;
                    row["Group Name"] = item.GroupName;
                    row["Benchmark"] = item.Benchmark;
                    _dataTable.Rows.Add(row);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(_dataTable, "All Master");

                    wb.SaveAs(foldername + "\\All Master.xlsx");

                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            return (true, "All Master file Downloaded!");
        }

        public async Task<(bool IsSuccess, string msg)> ProcessMaster(List<AllMasterModel> data, string foldername)
        {
            try
            {
                var _dataTable = new System.Data.DataTable();

                _dataTable.Columns.Add("Entity ID");
                _dataTable.Columns.Add("Account Number");
                _dataTable.Columns.Add("Start Date");
                _dataTable.Columns.Add("Sleeve Strategy");
                _dataTable.Columns.Add("Business Line");
                _dataTable.Columns.Add("Period Beginning Market Value");
                _dataTable.Columns.Add("Period Ending Market Value");
                _dataTable.Columns.Add("Period Performance Gross");
                _dataTable.Columns.Add("Period Performance Net");
                _dataTable.Columns.Add("Entity Path");
                _dataTable.Columns.Add("IsUMA");
                _dataTable.Columns.Add("Legacy CLS AID");
                _dataTable.Columns.Add("Net Flow");
                _dataTable.Columns.Add("Flow %");
                _dataTable.Columns.Add("Cash %");
                _dataTable.Columns.Add("Terminated");
                _dataTable.Columns.Add("Model Change");
                _dataTable.Columns.Add("Entity Name");
                _dataTable.Columns.Add("Group Name");
                _dataTable.Columns.Add("Benchmark");

                foreach (var item in data)
                {
                    var row = _dataTable.NewRow();
                    row["Entity ID"] = item.EntityID;
                    row["Account Number"] = item.AccountNumber;
                    row["Start Date"] = item.StartDate;
                    row["Sleeve Strategy"] = item.SleeveStrategy;
                    row["Business Line"] = item.BusinessLine;
                    row["Period Beginning Market Value"] = Convert.ToDouble(item.PeriodBeginningMarketValue?.ToString("G29"));
                    row["Period Ending Market Value"] = Convert.ToDouble(item.PeriodEndingMarketValue?.ToString("G29"));
                    row["Period Performance Gross"] = item.PeriodPerformanceGross;
                    row["Period Performance Net"] = item.PeriodPerformanceNet;
                    row["Entity Path"] = item.EntityPath;
                    row["IsUMA"] = item.IsUMA;
                    row["Legacy CLS AID"] = item.LegacyCLSAID;
                    row["Net Flow"] = Convert.ToDouble(item.NetFlow?.ToString("G29"));
                    row["Flow %"] = Convert.ToDouble(item.FlowPercentage?.ToString("G29"));
                    row["Cash %"] = Convert.ToDouble(item.CashPercentage?.ToString("G29"));
                    row["Terminated"] = item.Terminated;
                    row["Model Change"] = item.ModelChange;
                    row["Entity Name"] = item.EntityName;
                    row["Group Name"] = item.GroupName;
                    row["Benchmark"] = item.Benchmark;
                    _dataTable.Rows.Add(row);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(_dataTable, "Master");

                    wb.SaveAs(foldername + "\\Master.xlsx");

                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            return (true, "Master file Downloaded!");
        }

        public async Task<(bool IsSuccess, string msg)> ProcessNoComposite(List<AllMasterModel> data, string foldername)
        {
            try
            {
                var _dataTable = new System.Data.DataTable();

                _dataTable.Columns.Add("Entity ID");
                _dataTable.Columns.Add("Account Number");
                _dataTable.Columns.Add("Start Date");
                _dataTable.Columns.Add("Sleeve Strategy");
                _dataTable.Columns.Add("Business Line");
                _dataTable.Columns.Add("Period Beginning Market Value");
                _dataTable.Columns.Add("Period Ending Market Value");
                _dataTable.Columns.Add("Period Performance Gross");
                _dataTable.Columns.Add("Period Performance Net");
                _dataTable.Columns.Add("Entity Path");
                _dataTable.Columns.Add("IsUMA");
                _dataTable.Columns.Add("Legacy CLS AID");
                _dataTable.Columns.Add("Net Flow");
                _dataTable.Columns.Add("Flow %");
                _dataTable.Columns.Add("Cash %");
                _dataTable.Columns.Add("Terminated");
                _dataTable.Columns.Add("Model Change");
                _dataTable.Columns.Add("Entity Name");
                _dataTable.Columns.Add("Group Name");
                _dataTable.Columns.Add("Benchmark");

                foreach (var item in data)
                {
                    var row = _dataTable.NewRow();
                    row["Entity ID"] = item.EntityID;
                    row["Account Number"] = item.AccountNumber;
                    row["Start Date"] = item.StartDate;
                    row["Sleeve Strategy"] = item.SleeveStrategy;
                    row["Business Line"] = item.BusinessLine;
                    row["Period Beginning Market Value"] = Convert.ToDouble(item.PeriodBeginningMarketValue?.ToString("G29"));
                    row["Period Ending Market Value"] = Convert.ToDouble(item.PeriodEndingMarketValue?.ToString("G29"));
                    row["Period Performance Gross"] = item.PeriodPerformanceGross;
                    row["Period Performance Net"] = item.PeriodPerformanceNet;
                    row["Entity Path"] = item.EntityPath;
                    row["IsUMA"] = item.IsUMA;
                    row["Legacy CLS AID"] = item.LegacyCLSAID;
                    row["Net Flow"] = Convert.ToDouble(item.NetFlow?.ToString("G29"));
                    row["Flow %"] = Convert.ToDouble(item.FlowPercentage?.ToString("G29"));
                    row["Cash %"] = Convert.ToDouble(item.CashPercentage?.ToString("G29"));
                    row["Terminated"] = item.Terminated;
                    row["Model Change"] = item.ModelChange;
                    row["Entity Name"] = item.EntityName;
                    row["Group Name"] = item.GroupName;
                    row["Benchmark"] = item.Benchmark;
                    _dataTable.Rows.Add(row);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(_dataTable, "No Composite");

                    wb.SaveAs(foldername + "\\No Composite.xlsx");

                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            return (true, "No Composite file Downloaded!");
        }

        public async Task<(bool IsSuccess, string msg)> ProcessExclusions(List<AllMasterModel> data, string foldername)
        {
            try
            {
                var _dataTable = new System.Data.DataTable();

                _dataTable.Columns.Add("Entity ID");
                _dataTable.Columns.Add("Account Number");
                _dataTable.Columns.Add("Start Date");
                _dataTable.Columns.Add("Sleeve Strategy");
                _dataTable.Columns.Add("Business Line");
                _dataTable.Columns.Add("Period Beginning Market Value");
                _dataTable.Columns.Add("Period Ending Market Value");
                _dataTable.Columns.Add("Period Performance Gross");
                _dataTable.Columns.Add("Period Performance Net");
                _dataTable.Columns.Add("Entity Path");
                _dataTable.Columns.Add("IsUMA");
                _dataTable.Columns.Add("Legacy CLS AID");
                _dataTable.Columns.Add("Net Flow");
                _dataTable.Columns.Add("Flow %");
                _dataTable.Columns.Add("Cash %");
                _dataTable.Columns.Add("Terminated");
                _dataTable.Columns.Add("Model Change");
                _dataTable.Columns.Add("Entity Name");
                _dataTable.Columns.Add("Group Name");
                _dataTable.Columns.Add("Benchmark");
                _dataTable.Columns.Add("Remarks");

                foreach (var item in data)
                {
                    var row = _dataTable.NewRow();
                    row["Entity ID"] = item.EntityID;
                    row["Account Number"] = item.AccountNumber;
                    row["Start Date"] = item.StartDate;
                    row["Sleeve Strategy"] = item.SleeveStrategy;
                    row["Business Line"] = item.BusinessLine;
                    row["Period Beginning Market Value"] = Convert.ToDouble(item.PeriodBeginningMarketValue?.ToString("G29"));
                    row["Period Ending Market Value"] = Convert.ToDouble(item.PeriodEndingMarketValue?.ToString("G29"));
                    row["Period Performance Gross"] = item.PeriodPerformanceGross;
                    row["Period Performance Net"] = item.PeriodPerformanceNet;
                    row["Entity Path"] = item.EntityPath;
                    row["IsUMA"] = item.IsUMA;
                    row["Legacy CLS AID"] = item.LegacyCLSAID;
                    row["Net Flow"] = Convert.ToDouble(item.NetFlow?.ToString("G29"));
                    row["Flow %"] = Convert.ToDouble(item.FlowPercentage?.ToString("G29"));
                    row["Cash %"] = Convert.ToDouble(item.CashPercentage?.ToString("G29"));
                    row["Terminated"] = item.Terminated;
                    row["Model Change"] = item.ModelChange;
                    row["Entity Name"] = item.EntityName;
                    row["Group Name"] = item.GroupName;
                    row["Benchmark"] = item.Benchmark;
                    row["Remarks"] = await GetExclusionType(item?.ExclusionType);
                    _dataTable.Rows.Add(row);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(_dataTable, "Exclusions");

                    wb.SaveAs(foldername + "\\Exclusions.xlsx");

                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            return (true, "Exclusions file Downloaded!");
        }

        public async Task<string?> GetExclusionType(string? exclusionType)
        {
            switch (exclusionType)
            {
                case "001":
                    return "New Accounts";
                case "002":
                    return "Terminated";
                case "003":
                    return "Model Changes";
                case "004":
                    return "Cash Flow";
                case "005":
                    return "UMA High Cash";
                case "006":
                    return "High Cash";
                case "007":
                    return "High Cash";
                default:
                    return "";
            }
        }
    }
}
