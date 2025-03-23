using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    [Table("TerminatedAccounts", Schema = "dbo")]
    public class TerminatedAccountsModel
    {
        [Key] public int OrionRegistrationId { get; set; } 

        public int? AIMAccountId { get; set; } 

        public string? AccountNumber { get; set; } 

        public DateTime? GoDate { get; set; } 

        public DateTime? CloseDate { get; set; } 

        public Guid UploadID { get; set; }
    }
}
