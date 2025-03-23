using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    [Table("SleeveStrategy", Schema = "dbo")]
    public class SleeveStrategyModel
    {
        [Key] public int SID { get; set; }
        public string SleeveStrategyName { get; set; }
        public Guid UploadID { get; set; }
    }
}
