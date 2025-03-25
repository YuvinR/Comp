using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    [Table("ModelChanges", Schema = "dbo")]
    public class ModelChangesModel
    {
        [Key] public int ID { get; set; }
        public int OrionRegistrationId { get; set; }
        public string? AccountNumber { get; set; }
        public DateTime? GoDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int? AIMAccountID { get; set; }
        public string? OldManagerCode { get; set; }
        public string? OldStyleCode { get; set; }
        public string? OldManagerStyle { get; set; }
        public DateTime? OldEffectiveDate { get; set; }
        public DateTime? OldExpirationDate { get; set; }
        public string? ManagerCode { get; set; }
        public string? StyleCode { get; set; }
        public string? ManagerStyle { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Guid UploadID { get; set; }
    }
}
