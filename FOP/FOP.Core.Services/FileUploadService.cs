using ExcelDataReader;
using FOP.Core.Entities;
using FOP.Core.Entities.Interfaces;
using FOP.Core.Entities.Interfaces.Repository;
using FOP.Core.Models.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FOP.Core.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileUploadRepository _fileUploadRepository;
        public FileUploadService(IFileUploadRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }

        public async Task<(bool IsSuccess, string msg)> UploadFiles(string file, Guid uploadID)
        {
            try
            {
                    if (file.Contains(FileTypes.GrossPerformanceBatch))
                    {
                        return await UploadGrossPerformance(file,uploadID);
                    }
                    else if (file.Contains(FileTypes.NetPerformanceBatch))
                    {
                        await UploadNetPerformance(file, uploadID);
                    }
                    else if (file.Contains(FileTypes.PortfolioAuditAccs))
                    {
                        await UploadPortfolioAuditAccounts(file, uploadID);
                    }
                    else if (file.Contains(FileTypes.PortfolioAuditRegs))
                    {
                        await UploadPortfolioAuditRegistrations(file, uploadID);
                    }
                    else if (file.Contains(FileTypes.SleeveRegContributions))
                    {
                        await UploadSleevedRegistrationsContributions(file, uploadID);
                    }
                    else if (file.Contains(FileTypes.CashQuery))
                    {
                        await UploadCashQuery(file, uploadID);
                    }
                    else if (file.Contains(FileTypes.ModelChanges))
                    {
                        await UploadModelChanges(file, uploadID);
                    }
                    else if (file.Contains(FileTypes.TerminatedAccounts))
                    {
                        await UploadTerminatedAccounts(file, uploadID);
                    }
                    else if (file.Contains(FileTypes.SleeveStratergies))
                    {
                        await UploadSleeveStratergies(file, uploadID);
                    }
                else
                    {
                        // Log exception
                    }

                return (true,"Success!");
            }
            catch (Exception ex)
            {
                // Log exception
                return (false, ex.Message);
            }
        }

        // Uploading Gross Performance data
        private async Task<(bool IsSuccess, string msg)> UploadGrossPerformance(string file, Guid uploadID)
        {
            try
            {
                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (table) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                DataTable dtGrossPerformance = result.Tables[0];
                List<GrossPerformanceBatchModel> grossPerformanceBatch = new List<GrossPerformanceBatchModel>();

                foreach (DataRow row in dtGrossPerformance.Rows)
                {
                    var rowdata = row.ItemArray;
                    GrossPerformanceBatchModel grossPerformance = new GrossPerformanceBatchModel
                    {
                        EntityID = Convert.ToInt32(rowdata[0]),
                        EntityName = rowdata[1] != DBNull.Value ? rowdata[1].ToString() : null,
                        GroupName = rowdata[2] != DBNull.Value ? rowdata[2].ToString() : null,
                        Benchmark = rowdata[3] != DBNull.Value ? rowdata[3].ToString() : null,
                        PeriodBeginningMarketValue = rowdata[4] != DBNull.Value ? TruncateDecimal((decimal)Convert.ToDouble(rowdata[4], CultureInfo.InvariantCulture), 32, 18) : (decimal?)null,
                        PeriodEndingMarketValue = rowdata[5] != DBNull.Value ? TruncateDecimal((decimal)Convert.ToDouble(rowdata[5], CultureInfo.InvariantCulture), 32, 18) : (decimal?)null,
                        PeriodPerformance = rowdata[6] != DBNull.Value ? rowdata[6].ToString() : null,
                        EntityPath = rowdata[7] != DBNull.Value ? (string)rowdata[7] : null,
                        UploadID = uploadID
                    };
                    grossPerformanceBatch.Add(grossPerformance);
                    stream.Dispose();
                    stream.Close();
                }
                return await _fileUploadRepository.AddGrossPerformanceBatch(grossPerformanceBatch);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
                // Log exception
            }

        }

        // Uploading Net Performance data
        private async Task<(bool IsSuccess, string msg)> UploadNetPerformance(string file, Guid uploadID)
        {
            try
            {
                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (table) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                DataTable dtNetPerformance = result.Tables[0];
                List<NetPerformanceBatchModel> netPerformanceBatch = new List<NetPerformanceBatchModel>();

                foreach (DataRow row in dtNetPerformance.Rows)
                {
                    var rowdata = row.ItemArray;
                    NetPerformanceBatchModel netPerformance = new NetPerformanceBatchModel
                    {

                        EntityID = Convert.ToInt32(rowdata[0]),
                        EntityName = rowdata[1] != DBNull.Value ? rowdata[1].ToString() : null,
                        GroupName = rowdata[2] != DBNull.Value ? rowdata[2].ToString() : null,
                        Benchmark = rowdata[3] != DBNull.Value ? rowdata[3].ToString() : null,
                        PeriodBeginningMarketValue = rowdata[4] != DBNull.Value ? TruncateDecimal((decimal)Convert.ToDouble(rowdata[4], CultureInfo.InvariantCulture), 32, 18) : (decimal?)null,
                        PeriodEndingMarketValue = rowdata[5] != DBNull.Value ? TruncateDecimal((decimal)Convert.ToDouble(rowdata[5], CultureInfo.InvariantCulture), 32, 18) : (decimal?)null,
                        PeriodPerformance = rowdata[6] != DBNull.Value ? rowdata[6].ToString() : null,
                        EntityPath = rowdata[7] != DBNull.Value ? rowdata[7].ToString() : null,
                        UploadID = uploadID
                    };
                    netPerformanceBatch.Add(netPerformance);
                    stream.Dispose();
                    stream.Close();
                }
                return await _fileUploadRepository.AddNetPerformance(netPerformanceBatch);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
                // Log exception
            }
            //  await _fileUploadRepository.AddAsync(grossPerformanceBatch);
        }

        // Uploading Portfolio Audit Accounts data
        private async Task<(bool IsSuccess, string msg)> UploadPortfolioAuditAccounts(string file, Guid uploadID)
        {
            try
            {
                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (table) => new ExcelDataTableConfiguration()
                    {
                        
                        UseHeaderRow = true
                    },
                    UseColumnDataType = true
                });
                DataTable dtPortfolioAuditAccounts = result.Tables[0];
                List<PortfolioAuditAccountsModel> portfolioAuditAccounts = new List<PortfolioAuditAccountsModel>();

                foreach (DataRow row in dtPortfolioAuditAccounts.Rows)
                {
                    try
                    {
                        var startDate = new DateTime();
                        var rowdata = row.ItemArray;
                        var rawStartDate = rowdata[20] != DBNull.Value ? Convert.ToDouble(rowdata[20]) : (double?)null;
                        if (rawStartDate != null)
                        {
                             startDate = DateTime.FromOADate((double)rawStartDate);
                        }

                        PortfolioAuditAccountsModel auditAccounts = new PortfolioAuditAccountsModel
                        {
                            AccountID = Convert.ToInt32(rowdata[0]),
                            Active = rowdata[1] != DBNull.Value ? Convert.ToBoolean(rowdata[1]) : (bool?)null,
                            HouseholdID = rowdata[2] != DBNull.Value ? Convert.ToInt32(rowdata[2]) : 0,
                            Name = rowdata[3] != DBNull.Value ? rowdata[3].ToString() : null,
                            RegistrationID = rowdata[4] != DBNull.Value ? Convert.ToInt32(rowdata[4]) : 0,
                            AccountNumber = rowdata[5] != DBNull.Value ? rowdata[5].ToString() : null,
                            Custodian = rowdata[6] != DBNull.Value ? rowdata[6].ToString() : null,
                            AccountType = rowdata[7] != DBNull.Value ? rowdata[7].ToString() : null,
                            ManagementStyle = rowdata[8] != DBNull.Value ? rowdata[8].ToString() : null,
                            PerformanceReviewed = rowdata[9] != DBNull.Value ? rowdata[9].ToString() : null,
                            Model = rowdata[10] != DBNull.Value ? rowdata[10].ToString() : null,
                            CurrentValue = rowdata[11] != DBNull.Value ? TruncateDecimal((decimal)Convert.ToDouble(rowdata[11], CultureInfo.InvariantCulture), 32, 18) : (decimal?)null,
                            FundFamily = rowdata[12] != DBNull.Value ? rowdata[12].ToString() : null,
                            CashBalance = rowdata[13] != DBNull.Value ? TruncateDecimal((decimal)Convert.ToDouble(rowdata[13], CultureInfo.InvariantCulture), 32, 18) : (decimal?)null,
                            CustodialRepCode = rowdata[14] != DBNull.Value ? rowdata[14].ToString() : null,
                            LastName = rowdata[15] != DBNull.Value ? rowdata[15].ToString() : null,
                            HistoricalDataReady = rowdata[16] != DBNull.Value ? Convert.ToBoolean(rowdata[16]) : (bool?)null,
                            Managed = rowdata[17] != DBNull.Value ? Convert.ToBoolean(rowdata[17]) : (bool?)null,
                            SleeveStrategy = rowdata[18] != DBNull.Value ? rowdata[18].ToString() : null,
                            ModelAggID = rowdata[19] != DBNull.Value ? Convert.ToInt32(rowdata[19]) : 0,
                            StartDate = rowdata[20] != DBNull.Value ? startDate : null,
                            BusinessLine = rowdata[21] != DBNull.Value ? rowdata[21].ToString() : null,
                            LegacyCLSAID = rowdata[22] != DBNull.Value ? Convert.ToInt32(rowdata[22]) : 0,
                            UploadID = uploadID
                        };
                        portfolioAuditAccounts.Add(auditAccounts);
                        stream.Dispose();
                        stream.Close();
                    }
                    catch (Exception ex)
                    {
                        stream.Dispose();
                        stream.Close();
                        // Log exception
                    }
                }
                return await _fileUploadRepository.AddPortfolioAuditAccounts(portfolioAuditAccounts);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
                // Log exception
            }
            //  await _fileUploadRepository.AddAsync(grossPerformanceBatch);
        }

        // Uploading Portfolio Audit Registrations data
        private async Task<(bool IsSuccess, string msg)> UploadPortfolioAuditRegistrations(string file, Guid uploadID)
        {
            try
            {
                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (table) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                DataTable dtPortfolioAuditRegistrations = result.Tables[0];
                List<PortfolioAuditRegistrationsModel> portfolioAuditRegistrations = new List<PortfolioAuditRegistrationsModel>();

                foreach (DataRow row in dtPortfolioAuditRegistrations.Rows)
                {
                    try
                    {
                        var dob = new DateTime();
                        var rowdata = row.ItemArray;
                        var rawDob = rowdata[8] != DBNull.Value ? Convert.ToDouble(rowdata[8]) : (double?)null;
                        if (rawDob != null)
                        {
                            dob = DateTime.FromOADate((double)rawDob);
                        }
                        PortfolioAuditRegistrationsModel auditRegistrations = new PortfolioAuditRegistrationsModel
                        {
                            RegistrationID = Convert.ToInt32(rowdata[0]),
                            Active = rowdata[1] != DBNull.Value ? Convert.ToBoolean(rowdata[1]) : (bool?)null,
                            LastName = rowdata[2] != DBNull.Value ? rowdata[2].ToString() : null,
                            FirstName = rowdata[3] != DBNull.Value ? rowdata[3].ToString() : null,
                            Name = rowdata[4] != DBNull.Value ? rowdata[4].ToString() : null,
                            AccountType = rowdata[5] != DBNull.Value ? rowdata[5].ToString() : null,
                            CurrentValue = rowdata[6] != DBNull.Value ? TruncateDecimal((decimal)Convert.ToDouble(rowdata[6], CultureInfo.InvariantCulture), 32, 18) : (decimal?)null,
                            SSNTaxID = rowdata[7] != DBNull.Value ? rowdata[7].ToString() : null,
                            DOB = rowdata[8] != DBNull.Value ? dob : null,
                            HouseholdID = rowdata[9] != DBNull.Value ? Convert.ToInt32(rowdata[9]) : 0,
                            MissingInformation = rowdata[10] != DBNull.Value ? rowdata[10].ToString() : null,
                            InvestmentObjective = rowdata[11] != DBNull.Value ? rowdata[11].ToString() : null,
                            SleeveStrategy = rowdata[12] != DBNull.Value ? rowdata[12].ToString() : null,
                            UMA = rowdata[13] != DBNull.Value ? Convert.ToBoolean(rowdata[13]) : (bool?)null,
                            CustodialAccountNumber = rowdata[14] != DBNull.Value ? rowdata[14].ToString() : null,
                            UploadID = uploadID
                        };
                        portfolioAuditRegistrations.Add(auditRegistrations);
                        stream.Dispose();
                        stream.Close();
                    }
                    catch (Exception ex)
                    {
                        stream.Dispose();
                        stream.Close();
                    }
                }
                return await _fileUploadRepository.AddPortfolioAuditRegistrations(portfolioAuditRegistrations);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Uploading Sleeved Registrations Contributions data
        private async Task<(bool IsSuccess, string msg)> UploadSleevedRegistrationsContributions(string file, Guid uploadID)
        {
            try
            {
                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (table) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                DataTable dtSleevedRegistrationsContributions = result.Tables[0];
                List<SleevedRegistrationsContributionsModel> sleevedRegistrationsContributions = new List<SleevedRegistrationsContributionsModel>();

                foreach (DataRow row in dtSleevedRegistrationsContributions.Rows)
                {
                    try
                    {
                        var rowdata = row.ItemArray;
                        SleevedRegistrationsContributionsModel sleevedRegistrations = new SleevedRegistrationsContributionsModel
                        {
                            Id =Guid.NewGuid(),
                            HHID = Convert.ToInt32(rowdata[0]),
                            RegID = Convert.ToInt32(rowdata[1]),
                            ClientName = rowdata[2] != DBNull.Value ? rowdata[2].ToString() : null,
                            BrokerDealerName = rowdata[3] != DBNull.Value ? rowdata[3].ToString() : null,
                            CustodialAccountNumber = rowdata[4] != DBNull.Value ? rowdata[4].ToString() : null,
                            TransactionType = rowdata[5] != DBNull.Value ? rowdata[5].ToString() : null,
                            Amount = rowdata[6] != DBNull.Value ? (float)Convert.ToDouble(rowdata[6], CultureInfo.InvariantCulture) : (float?)null,
                            RepID = rowdata[7] != DBNull.Value ? Convert.ToInt32(rowdata[7]) : 0,
                            Date = rowdata[8] != DBNull.Value && rowdata[8] is DateTime ? (DateTime?)Convert.ToDateTime(rowdata[8], CultureInfo.InvariantCulture) : null,
                            TransactionStatus = rowdata[9] != DBNull.Value ? rowdata[9].ToString() : null,
                            UploadID = uploadID
                        };
                        sleevedRegistrationsContributions.Add(sleevedRegistrations);
                        stream.Dispose();
                        stream.Close();
                    }
                    catch (Exception ex)
                    {
                        stream.Dispose();
                        stream.Close();
                        // Log exception
                    }
                }
                return await _fileUploadRepository.AddSleevedRegistrationsContributions(sleevedRegistrationsContributions);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
                // Log exception
            }
            //  await _fileUploadRepository.AddAsync(grossPerformanceBatch);
        }

        // Uploading Cash Query data
        private async Task<(bool IsSuccess, string msg)> UploadCashQuery(string file, Guid uploadID)
        {
            try
            {
                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (table) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                DataTable dtCashQuerie = result.Tables[0];
                List<CashQueryModel> cashQuerie = new List<CashQueryModel>();

                foreach (DataRow row in dtCashQuerie.Rows)
                {
                    try
                    {
                        var rowdata = row.ItemArray;
                        CashQueryModel cashQueryModel = new CashQueryModel
                        {
                            ClientLastName = rowdata[0] != DBNull.Value ? (string)rowdata[0] : null,
                            RegistrationID = Convert.ToInt32(rowdata[1]),
                            RegistrationName = rowdata[2] != DBNull.Value ? (string)rowdata[2] : null,
                            RegistrationCode = rowdata[3] != DBNull.Value ? (string)rowdata[3] : null,
                            RegistrationValue = rowdata[4] != DBNull.Value ? TruncateDecimal((decimal)Convert.ToDouble(rowdata[4], CultureInfo.InvariantCulture), 32, 18) : (decimal?)null,
                            MoneyMarketValue = rowdata[5] != DBNull.Value ? TruncateDecimal((decimal)Convert.ToDouble(rowdata[5], CultureInfo.InvariantCulture), 32, 18) : (decimal?)null,
                            RegistrationPercentageInCash = rowdata[6] != DBNull.Value ? TruncateDecimal((decimal)Convert.ToDouble(rowdata[6], CultureInfo.InvariantCulture), 32, 18) : (decimal?)null,
                            RepName = rowdata[7] != DBNull.Value ? (string)rowdata[7] : null,
                            RepNo = rowdata[8] != DBNull.Value ? (string)rowdata[8] : null,
                            SleeveStrategy = rowdata[9] != DBNull.Value ? (string)rowdata[9] : null,
                            UploadID = uploadID
                        };
                        cashQuerie.Add(cashQueryModel);
                        stream.Dispose();
                        stream.Close();
                    }
                    catch (Exception ex)
                    {
                        stream.Dispose();
                        stream.Close();
                    }
                }
                return await _fileUploadRepository.AddCashQuery(cashQuerie);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Uploading Model Changes data
        private async Task<(bool IsSuccess, string msg)> UploadModelChanges(string file, Guid uploadID)
        {
            try
            {
                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (table) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                DataTable dtModelChanges = result.Tables[0];
                List<ModelChangesModel> modelChanges = new List<ModelChangesModel>();

                foreach (DataRow row in dtModelChanges.Rows)
                {
                    try
                    {

                        var rowdata = row.ItemArray;
                        var GoDate = rowdata[2] != DBNull.Value ? Convert.ToDateTime(rowdata[2]) : (DateTime?)null;
                        var CloseDate  = rowdata[3] != DBNull.Value ? Convert.ToDateTime(rowdata[3]) : (DateTime?)null;
                        var OldEffectiveDate  = rowdata[8] != DBNull.Value ? Convert.ToDateTime(rowdata[8]) : (DateTime?)null;
                        var OldExpirationDate  = rowdata[9] != DBNull.Value ? Convert.ToDateTime(rowdata[9]) : (DateTime?)null;
                        var EffectiveDate  = rowdata[13] != DBNull.Value ? Convert.ToDateTime(rowdata[13]) : (DateTime?)null;
                        var ExpirationDate  = rowdata[14] != DBNull.Value ? Convert.ToDateTime(rowdata[14]) : (DateTime?)null;

                        ModelChangesModel modelChangesModel = new ModelChangesModel
                        {
                            AccountNumber = rowdata[0] != DBNull.Value ? rowdata[0].ToString() : null,
                            OrionRegistrationId = rowdata[1] != DBNull.Value ? Convert.ToInt32(rowdata[1]) : 0 ,
                            GoDate = rowdata[2] != DBNull.Value ? GoDate : null,
                            CloseDate = rowdata[3] != DBNull.Value ? CloseDate : null,
                            AIMAccountID = rowdata[4] != DBNull.Value ? Convert.ToInt32(rowdata[4]) : 0,
                            OldManagerCode = rowdata[5] != DBNull.Value ? rowdata[5].ToString() : null,
                            OldStyleCode = rowdata[6] != DBNull.Value ? rowdata[6].ToString() : null,
                            OldManagerStyle = rowdata[7] != DBNull.Value ? rowdata[7].ToString() : null,
                            OldEffectiveDate = rowdata[8] != DBNull.Value ? OldEffectiveDate : null,
                            OldExpirationDate = rowdata[9] != DBNull.Value ? OldExpirationDate : null,
                            ManagerCode = rowdata[10] != DBNull.Value ? rowdata[10].ToString() : null,
                            StyleCode = rowdata[11] != DBNull.Value ? rowdata[11].ToString() : null,
                            ManagerStyle = rowdata[12] != DBNull.Value ? rowdata[12].ToString() : null,
                            EffectiveDate = rowdata[13] != DBNull.Value ? EffectiveDate : null,
                            ExpirationDate = rowdata[14] != DBNull.Value ? ExpirationDate : null,
                            UploadID = uploadID

                        };
                        modelChanges.Add(modelChangesModel);
                        stream.Dispose();
                        stream.Close();
                    }
                    catch (Exception ex)
                    {
                        stream.Dispose();
                        stream.Close();
                    }
                }
                return await _fileUploadRepository.AddModelChange(modelChanges);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Uploading Terminated Accounts data
        private async Task<(bool IsSuccess, string msg)> UploadTerminatedAccounts(string file, Guid uploadID)
        {
            try
            {
                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (table) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                DataTable dtterminatedAccountsModels = result.Tables[0];
                List<TerminatedAccountsModel> terminatedAccountsModels = new List<TerminatedAccountsModel>();

                foreach (DataRow row in dtterminatedAccountsModels.Rows)
                {
                    try
                    {

                        var rowdata = row.ItemArray;
                        var GoDate = rowdata[3] != DBNull.Value ? Convert.ToDateTime(rowdata[3]) : (DateTime?)null;
                        var CloseDate = rowdata[4] != DBNull.Value ? Convert.ToDateTime(rowdata[4]) : (DateTime?)null;
                        TerminatedAccountsModel terminatedAccounts = new TerminatedAccountsModel
                        {
                            AIMAccountId = Convert.ToInt32(rowdata[0]),
                            AccountNumber = rowdata[1] != DBNull.Value ? rowdata[1].ToString() : null,
                            OrionRegistrationId = rowdata[2] != DBNull.Value ? Convert.ToInt32(rowdata[2]) : 0,
                            GoDate = rowdata[3] != DBNull.Value ? GoDate : null,
                            CloseDate = rowdata[4] != DBNull.Value ? CloseDate : null,
                            UploadID = uploadID
                        };
                        terminatedAccountsModels.Add(terminatedAccounts);
                        stream.Dispose();
                        stream.Close();
                    }
                    catch (Exception ex)
                    {
                        stream.Dispose();
                        stream.Close();
                        // Log exception
                    }
                }
                return await _fileUploadRepository.AddTerminatedAccounts(terminatedAccountsModels);
            }
            catch (Exception ex)
            {
                return (false, ex.Message); 
                // Log exception
            }
            //  await _fileUploadRepository.AddAsync(grossPerformanceBatch);
        }

        // Uploading Sleeve Stratergies data
        private async Task<(bool IsSuccess, string msg)> UploadSleeveStratergies(string file, Guid uploadID)
        {
            try
            {
                List<SleeveStrategyModel> dtSleeveStrategy = new List<SleeveStrategyModel>();

                using (StreamReader readerTextFile = new StreamReader(file))
                {
                    while (!readerTextFile.EndOfStream)
                    {
                        string strategy = readerTextFile.ReadLine().Trim();
                        if (strategy != "")
                        {
                            dtSleeveStrategy.Add(new SleeveStrategyModel { SleeveStrategyName = strategy , UploadID = uploadID });
                        }
                    }
                }           
                return await _fileUploadRepository.AddSleeveStrategy(dtSleeveStrategy);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
                // Log exception
            }
            //  await _fileUploadRepository.AddAsync(grossPerformanceBatch);
        }

        private decimal? TruncateDecimal(decimal? value, int precision, int scale)
        {
            try
            {
                if (value.HasValue)
                {
                    decimal factor = (decimal)Math.Pow(10, scale);
                    return Math.Truncate(value.Value * factor) / factor;
                }
                return value;
            }
            catch (Exception ex)
            {
                return 0;
                // Log exception
            }
        }

    }
}


