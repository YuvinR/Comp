using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    public class SleevedRegistrationsContributionsModel
    {
        [Key] public Guid Id { get; set; }
        public int? HHID { get; set; } 

        public int RegID { get; set; } 

        public string? ClientName { get; set; } 

        public string? BrokerDealerName { get; set; } 

        public string? CustodialAccountNumber { get; set; } 

        public string? TransactionType { get; set; } 

        public float? Amount { get; set; } 

        public int? RepID { get; set; } 

        public DateTime? Date { get; set; } 

        public string? TransactionStatus { get; set; } 

        public Guid UploadID { get; set; }
    }
}
