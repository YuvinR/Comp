using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    [Table("CashQuery", Schema = "dbo")]
    public class CashQueryModel
    {
        [Key] public int ID { get; set; }
        public int RegistrationID { get; set; }
        public string ClientLastName { get; set; }
        public string RegistrationName { get; set; }
        public string RegistrationCode { get; set; }
        public decimal? RegistrationValue { get; set; }
        public decimal? MoneyMarketValue { get; set; }
        public decimal? RegistrationPercentageInCash { get; set; }
        public string RepName { get; set; }
        public string RepNo { get; set; }
        public string SleeveStrategy { get; set; }
        public Guid UploadID { get; set; }
    }
}
