using EFCore.BulkExtensions;
using FOP.Core.Entities;
using FOP.Core.Entities.Interfaces.Repository;
using FOP.Infrastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FOP.Infrastructure.Data.Repository
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly DBContext _context;

        public FileUploadRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<(bool IsSuccess, string msg)> AddGrossPerformanceBatch(List<GrossPerformanceBatchModel> grossPerformanceBatch)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.GrossPerformanceBatch.AddRangeAsync(grossPerformanceBatch);
                    var res = await _context.SaveChangesAsync();
                   await transaction.CommitAsync();
                    return (res > 0,"");
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    return (false,ex.Message);
                }
           }
        }

        public async Task<(bool IsSuccess, string msg)> AddNetPerformance(List<NetPerformanceBatchModel> netPerformanceBatch)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.NetPerformanceBatches.AddRangeAsync(netPerformanceBatch);
                    var res = await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return (res > 0, "");
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    return (false, ex.Message);
                }
            }
        }

        public async Task<(bool IsSuccess, string msg)> AddPortfolioAuditAccounts(List<PortfolioAuditAccountsModel> portfolioAuditAccount)
        {
            try
                {
                    await _context.BulkInsertAsync(portfolioAuditAccount);
                    return (true, "");
                }
                catch (System.Exception ex)
                {
                    return (false, ex.Message);
                }
        }

        public async Task<(bool IsSuccess, string msg)> AddPortfolioAuditRegistrations(List<PortfolioAuditRegistrationsModel> portfolioAuditRegistration)
        {
                try
                {
                    await _context.BulkInsertAsync(portfolioAuditRegistration);
                    return (true, "");
                }
                catch (System.Exception ex)
                {
                    return (false, ex.Message);
                }
        }

        public async Task<(bool IsSuccess, string msg)> AddSleevedRegistrationsContributions(List<SleevedRegistrationsContributionsModel> sleevedRegistrationsContributions)
        {
            try
            {

                await _context.BulkInsertAsync(sleevedRegistrationsContributions);
                return (true, "");
            }
            catch (System.Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public async Task<(bool IsSuccess, string msg)> AddCashQuery(List<CashQueryModel> cashQueries)
        {
            try
            {
                await _context.BulkInsertAsync(cashQueries);
                return (true, "");
            }
            catch (System.Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public async Task<(bool IsSuccess, string msg)> AddModelChange(List<ModelChangesModel> modelChanges)
        {
            try
            {
                await _context.BulkInsertAsync(modelChanges);
                return (true, "");
            }
            catch (System.Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public async Task<(bool IsSuccess, string msg)> AddTerminatedAccounts(List<TerminatedAccountsModel> terminatedAccounts)
        {
            try
            {

                await _context.BulkInsertAsync(terminatedAccounts);
                return (true, "");
            }
            catch (System.Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public async Task<(bool IsSuccess, string msg)> AddSleeveStrategy(List<SleeveStrategyModel> sleeveStrategies)
        {
            try
            {

                await _context.sleeveStrategies.AddRangeAsync(sleeveStrategies);
                var res = await _context.SaveChangesAsync();
                return (res > 0, "");
            }
            catch (System.Exception ex)
            {
                return (false, ex.Message);
            }

        }
    }
}
