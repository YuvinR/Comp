using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities.Interfaces.Repository
{
    public interface IFileUploadRepository
    {
        //custom methods
        Task<(bool IsSuccess , string msg)> AddGrossPerformanceBatch(List<GrossPerformanceBatchModel> grossPerformanceBatch);
        Task<(bool IsSuccess, string msg)> AddNetPerformance(List<NetPerformanceBatchModel> netPerformanceBatch);
        Task<(bool IsSuccess, string msg)> AddPortfolioAuditAccounts(List<PortfolioAuditAccountsModel> portfolioAuditAccount);
        Task<(bool IsSuccess, string msg)> AddPortfolioAuditRegistrations(List<PortfolioAuditRegistrationsModel> portfolioAuditRegistration);
        Task<(bool IsSuccess, string msg)> AddSleevedRegistrationsContributions(List<SleevedRegistrationsContributionsModel> sleevedRegistrationsContribution);
        Task<(bool IsSuccess, string msg)> AddCashQuery(List<CashQueryModel> cashQueries);
        Task<(bool IsSuccess, string msg)> AddModelChange(List<ModelChangesModel> modelChange);
        Task<(bool IsSuccess, string msg)> AddTerminatedAccounts(List<TerminatedAccountsModel> terminatedAccount);
        Task<(bool IsSuccess, string msg)> AddSleeveStrategy(List<SleeveStrategyModel> sleeveStrategies);

    }
}
