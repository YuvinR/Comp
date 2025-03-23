using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Models.Utils
{
    public class FileTypes
    {
        public static string SleeveStratergies = "sleeve_strategies";
        public static string GrossPerformanceBatch = "Gross Performance Batch";
        public static string NetPerformanceBatch = "Net Performance Batch";
        public static string PortfolioAuditAccs = "Portfolio Audit Accounts";
        public static string PortfolioAuditRegs = "Portfolio Audit Registrations";
        public static string SleeveRegContributions = "Sleeved Registrations Contributions";
        public static string CashQuery = "Cash Query";
        public static string ModelChanges = "Model Changes";
        public static string TerminatedAccounts = "Terminated Accounts";
        public static string AllMaster = "All Master";
    }

    public enum DownloadFiles
    {
        AllMaster,
        Master,
        Orion,
        NoComposites,
        Exclusions

    }
}
