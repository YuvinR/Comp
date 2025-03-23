using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    [Table("MonthlyUploads", Schema = "dbo")]
    public class MonthlyUploadsModel
    {
        [Key] public Guid MonthlyUploadID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
