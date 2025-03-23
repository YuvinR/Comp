using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOP.Core.Entities;
using FOP.Core.Entities.Interfaces.Repository;
using FOP.Core.Models;
using FOP.Core.Models.Utils;
using FOP.Infrastructure.Data.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace FOP.Infrastructure.Data.Repository
{
    public class AllMasterRepository : IAllMasterRepository
    {
        private readonly DBContext _context;

        public AllMasterRepository(DBContext context)
        {
            _context = context;
        }



        public async Task<bool> CreateAllMaster(int year, int month)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var monthlyUpload = await _context.monthlyUploads
                        .FirstOrDefaultAsync(mu => mu.Year == year && mu.Month == month);

                    if (monthlyUpload == null)
                    {
                        return false;
                    }

                    // Insert into AllMasterModels
                    var result = await _context.Database.ExecuteSqlRawAsync(insertsql, new SqlParameter("@UploadID", monthlyUpload.MonthlyUploadID));

                    if (result>0)
                    {
                        await transaction.CommitAsync();
                        return true;
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<(bool IsSuccess, string msg)> CreateMaster(int year, int month, DynamicLimitsModel dynamicLimitsModel)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var monthlyUpload = await _context.monthlyUploads
                        .FirstOrDefaultAsync(mu => mu.Year == year && mu.Month == month);

                    if (monthlyUpload == null)
                    {
                        return (false, "Upload ID not found!");
                    }

                            // update orions
                            var resultOrion = await _context.Database.ExecuteSqlRawAsync(updateOrions, new SqlParameter("@UploadID", monthlyUpload.MonthlyUploadID));
                   
                            // update no composite
                            var resultNoComposite = await _context.Database.ExecuteSqlRawAsync(updateNoComposite, 
                                new SqlParameter("@UploadID", monthlyUpload.MonthlyUploadID), 
                                new SqlParameter("@Test", dynamicLimitsModel.EntityTestValue));

                            // exclusions

                            // New Account
                            var exclusionsNewAcc = await _context.Database.ExecuteSqlRawAsync(updateNewAccount, 
                                new SqlParameter("@UploadID", monthlyUpload.MonthlyUploadID));

                            // Terminated
                            var exclusionsTerminated = await _context.Database.ExecuteSqlRawAsync(updateTerminated, 
                                new SqlParameter("@UploadID", monthlyUpload.MonthlyUploadID), 
                                new SqlParameter("@Year", year), 
                                new SqlParameter("@Month", month));

                            // Model Changes
                            var exclusionsModelChanges = await _context.Database.ExecuteSqlRawAsync(updateModelChanges,
                                new SqlParameter("@UploadID", monthlyUpload.MonthlyUploadID),
                                new SqlParameter("@Year", year), 
                                new SqlParameter("@Month", month));

                            // Cash flow
                            var exclusionsCashFlow = await _context.Database.ExecuteSqlRawAsync(updateCashFlow,
                                new SqlParameter("@UploadID", monthlyUpload.MonthlyUploadID),
                                new SqlParameter("@CashMax", dynamicLimitsModel.MaxFlowPercentage),
                                new SqlParameter("@CashMin", dynamicLimitsModel.MinFlowPercentage));


                            // UMA High Cash
                            var exclusionsUMAHighCash = await _context.Database.ExecuteSqlRawAsync(updateUMAHighCash, 
                                new SqlParameter("@UploadID", monthlyUpload.MonthlyUploadID),
                                new SqlParameter("@UMACash", dynamicLimitsModel.CashUMAPercentage));

                            // High Cash 1
                            var exclusionsHighCash1 = await _context.Database.ExecuteSqlRawAsync(updateHighCash1,
                                new SqlParameter("@UploadID", monthlyUpload.MonthlyUploadID),
                                new SqlParameter("@CashOver", dynamicLimitsModel.CashOverPercentage));

                            // High Cash 2
                            var exclusionsHighCash2 = await _context.Database.ExecuteSqlRawAsync(updateHighCash2,
                                        new SqlParameter("@UploadID", monthlyUpload.MonthlyUploadID),
                                        new SqlParameter("@CashOverDest", dynamicLimitsModel.CashOverDestPercentage));

                            await transaction.CommitAsync();
                            return (true, "All exclusions updated successfully");

                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<AllMasterModel>> GetDownloadContent(DownloadFiles downloadFileType, Guid uploadId)
        {
            switch (downloadFileType)
            {
                case DownloadFiles.AllMaster:
                    return await _context.allMasterModels.Where(am => am.UploadId == uploadId).ToListAsync();

                case DownloadFiles.Master:
                    return await _context.allMasterModels.Where(am => am.UploadId == uploadId && am.NoComposite != true && am.ToExclusions != true).ToListAsync();

                case DownloadFiles.Orion:
                    return await _context.allMasterModels.Where(am => am.UploadId == uploadId && am.NoComposite == true && am.NoCompositeType == 1).ToListAsync();

                case DownloadFiles.NoComposites:
                    return await _context.allMasterModels.Where(am => am.UploadId == uploadId && am.NoComposite == true && am.NoCompositeType == 2).ToListAsync();

                case DownloadFiles.Exclusions:
                    return await _context.allMasterModels.Where(am => am.UploadId == uploadId && (am.ExclusionType == "001" || am.ExclusionType == "002" || am.ExclusionType == "003" || am.ExclusionType == "004" || am.ExclusionType == "005" || am.ExclusionType == "006" || am.ExclusionType == "007")).ToListAsync();

                default:
                    return null;
            }
        }
           



            #region SQL Queries All master to Master
            string insertsql = @"
               WITH OrderedAccounts AS (
                    SELECT 
                        PA.RegistrationID,
                        PA.LegacyCLSAID,
                        PA.AccountNumber,
                        PA.StartDate,
                        PA.BusinessLine,
                        PA.SleeveStrategy,
                        ROW_NUMBER() OVER (PARTITION BY PA.RegistrationID ORDER BY PA.AccountNumber ASC) AS RowNum
                    FROM 
                        [dbo].[PortfolioAuditAccounts] AS PA
                    WHERE 
                        PA.UploadID = @UploadID
                )

                INSERT INTO dbo.AllMaster (EntityID, AccountNumber, StartDate, SleeveStrategy, BusinessLine, PeriodBeginningMarketValue, PeriodEndingMarketValue, PeriodPerformanceGross, PeriodPerformanceNet, EntityPath, IsUMA, LegacyCLSAID, 
                                         NetFlow, FlowPercentage, CashPercentage, Terminated, ModelChange, EntityName, GroupName, Benchmark,UploadID ,NoComposite , NoCompositeType , ToExclusions , ExclusionType)

                SELECT 
		                GP.EntityId,
		                PAA.AccountNumber,
		                PAA.StartDate,
		                PAA.SleeveStrategy,
		                PAA.BusinessLine,
		                GP.PeriodBeginningMarketValue ,
		                GP.PeriodEndingMarketValue,
		                GP.PeriodPerformance as [PeriodPerformanceGross],
		                NP.PeriodPerformance as [PeriodPerformanceNet],
		                GP.EntityPath,
		                PAR.UMA as [IsUMA],
		                PAA.LegacyCLSAID,
		                SRC.Amount as NetFlow ,
		                ((SRC.Amount / NULLIF(GP.PeriodBeginningMarketValue,0.000000000000000000 ))* 100) as FlowPercentage,
		                CQ.RegistrationPercentageInCash as [CashPercentage],
		                TA.CloseDate as [Terminated] , 
		                MC.OldExpirationDate as [ModelChange],
		                GP.EntityName , 
		                GP.GroupName , 
		                GP.Benchmark ,
		                @UploadID,
		                'false',
		                0,
		                'false',
		                ''
                FROM [dbo].[GrossPerformanceBatch] as GP
		                LEFT JOIN [dbo].[NetPerformanceBatch] as NP ON GP.EntityID = NP.EntityID AND NP.UploadID = @UploadID
		                LEFT JOIN [dbo].[PortfolioAuditRegistrations] as PAR ON PAR.RegistrationID = GP.EntityID  AND PAR.UploadID = @UploadID
		                LEFT JOIN  OrderedAccounts as PAA ON PAA.RegistrationID = GP.EntityID AND PAA.RowNum = 1
		                LEFT JOIN 
		                (select SR.RegID ,SUM(SR.Amount) as Amount from [dbo].[SleevedRegistrationsContributions] SR WHERE SR.UploadID = @UploadID AND RegID is not null group by SR.RegID)
		                as SRC ON SRC.RegID = GP.EntityID 
		 
		                LEFT JOIN [dbo].[CashQuery] as CQ ON CQ.RegistrationID = GP.EntityID AND CQ.UploadID = @UploadID
		                LEFT JOIN [dbo].[TerminatedAccounts] as TA ON TA.OrionRegistrationId = GP.EntityID AND TA.UploadID = @UploadID
		                LEFT JOIN (select M.OrionRegistrationId ,MAX(M.OldExpirationDate) as OldExpirationDate from [dbo].[ModelChanges] M WHERE M.UploadID = @UploadID AND M.OrionRegistrationId is not null group by M.OrionRegistrationId) 
		                as MC ON MC.OrionRegistrationId = GP.EntityID ";

         string updateOrions = @"update dbo.AllMaster SET NoComposite = 1 , NoCompositeType = 1 WHERE SleeveStrategy like 'Orion %' AND UploadID = @UploadID";

         string updateNoComposite = @"update dbo.AllMaster SET NoComposite = 1 , NoCompositeType = 2 WHERE( NoCompositeType != 1  )AND ((BusinessLine like 'CLS - %') 
                                       OR ( (COALESCE(SleeveStrategy, '') = '' OR LEN(TRIM(SleeveStrategy)) = 0) or NOT SleeveStrategy IN (SELECT [SleeveStrategyName] FROM [dbo].[SleeveStrategy] )) 
                                       OR (PATINDEX('%[^a-zA-Z]test[^a-zA-Z]%', ' ' + EntityName + ' ') > 0 AND PeriodBeginningMarketValue < @Test)) 
								       AND (UploadID = @UploadID )";

         string updateNewAccount = @"update dbo.AllMaster SET ToExclusions = 1 , ExclusionType = '001'
                                        WHERE (PeriodBeginningMarketValue = 0.000000000000000000) AND UploadID = @UploadID AND  NoComposite != 1";

         string updateTerminated = @"update dbo.AllMaster SET ToExclusions = 1 , ExclusionType = '002'
                                        WHERE (DATEPART(MONTH,Terminated) = @Month AND DATEPART(YEAR,Terminated) = @Year)  AND UploadID = @UploadID AND  NoComposite != 1  AND ToExclusions != 1 AND  ExclusionType != '001'";
         
         string updateModelChanges = @"update dbo.AllMaster SET ToExclusions = 1 , ExclusionType = '003' 
                                        WHERE (DATEPART(MONTH,ModelChange) = @Month AND DATEPART(YEAR,ModelChange) = @Year)  AND UploadID = @UploadID AND NoComposite != 1  AND ToExclusions != 1 AND  ExclusionType NOT IN( '001','002')";
         
         string updateCashFlow = @"update dbo.AllMaster SET ToExclusions = 1 , ExclusionType = '004' 
                                    WHERE (FlowPercentage > @CashMax or FlowPercentage < -@CashMin) AND UploadID = @UploadID AND NoComposite != 1  AND ToExclusions != 1 AND  ExclusionType NOT IN( '001','002','003')";
        
         string updateUMAHighCash = @"update dbo.AllMaster SET ToExclusions = 1 , ExclusionType = '005' WHERE (CashPercentage >@UMACash and IsUMA = 1 and SleeveStrategy NOT LIKE 'Destinations Defensive%') AND UploadID = @UploadID	
		                            AND NoComposite != 1  AND ToExclusions != 1 AND  ExclusionType NOT IN( '001','002','003','004')";
         
         string updateHighCash1 = @"update dbo.AllMaster SET ToExclusions = 1 , ExclusionType = '006' 
                                   WHERE  (CashPercentage >@CashOver and SleeveStrategy LIKE 'Destinations Defensive%') AND UploadID = @UploadID
								   AND NoComposite != 1  AND ToExclusions != 1 AND  ExclusionType NOT IN( '001','002','003','004','005')";

         string updateHighCash2 = @"update dbo.AllMaster SET ToExclusions = 1 , ExclusionType = '007' 
                                   WHERE  (CashPercentage >@CashOverDest and SleeveStrategy LIKE 'Destinations%' AND SleeveStrategy NOT LIKE 'Destinations Defensive%') AND UploadID = @UploadID
								   AND NoComposite != 1  AND ToExclusions != 1 AND  ExclusionType NOT IN( '001','002','003','004','005','006' )";
        #endregion

        #region Get Download List


        #endregion
    }
}
