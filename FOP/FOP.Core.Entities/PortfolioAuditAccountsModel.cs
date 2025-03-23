using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    [Table("PortfolioAuditAccounts", Schema = "dbo")]
    public class PortfolioAuditAccountsModel
    {
        [Key] public int? AccountID { get; set; } 

        public bool? Active { get; set; } 

        public int? HouseholdID { get; set; } 

        public string? Name { get; set; } 

        public int? RegistrationID { get; set; } 

        public string? AccountNumber { get; set; } 

        public string? Custodian { get; set; }

        public string? AccountType { get; set; } 

        public string? ManagementStyle { get; set; } 

        public string? PerformanceReviewed { get; set; } 

        public string? Model { get; set; }

        [Column(TypeName = "decimal(32, 18)")]
        public decimal? CurrentValue { get; set; } 

        public string? FundFamily { get; set; }

        [Column(TypeName = "decimal(32, 18)")]
        public decimal? CashBalance { get; set; } 

        public string? CustodialRepCode { get; set; } 

        public string? LastName { get; set; } 

        public bool? HistoricalDataReady { get; set; } 

        public bool? Managed { get; set; } 

        public string? SleeveStrategy { get; set; } 

        public int? ModelAggID { get; set; } 

        public DateTime? StartDate { get; set; } 

        public string? BusinessLine { get; set; } 

        public int? LegacyCLSAID { get; set; } 

        public Guid UploadID { get; set; } 
    }
}
