using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    [Table("PortfolioAuditRegistrations", Schema = "dbo")]
    public class PortfolioAuditRegistrationsModel
    {
        [Key] public int ID { get; set; }

        public int RegistrationID { get; set; } 

        public bool? Active { get; set; } 

        public string? LastName { get; set; } 

        public string? FirstName { get; set; } 

        public string? Name { get; set; } 

        public string? AccountType { get; set; }

        [Column(TypeName = "decimal(32, 18)")]
        public decimal? CurrentValue { get; set; } 

        public string? SSNTaxID { get; set; } 

        public DateTime? DOB { get; set; } 

        public int? HouseholdID { get; set; } 

        public string? MissingInformation { get; set; } 

        public string? InvestmentObjective { get; set; } 

        public string? SleeveStrategy { get; set; } 

        public bool? UMA { get; set; } 

        public string? CustodialAccountNumber { get; set; } 

        public Guid UploadID { get; set; }
    }
}
