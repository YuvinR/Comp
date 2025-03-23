using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    [Table("AllMaster", Schema = "dbo")]
    public class AllMasterModel
    {
        [Key]public int EntityID { get; set; }
        public string? AccountNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public string? SleeveStrategy { get; set; }
        public string? BusinessLine { get; set; }
        public decimal? PeriodBeginningMarketValue { get; set; }
        public decimal? PeriodEndingMarketValue { get; set; }
        public string? PeriodPerformanceGross { get; set; }
        public string? PeriodPerformanceNet { get; set; }
        public string? EntityPath { get; set; }
        public bool? IsUMA { get; set; }
        public int? LegacyCLSAID { get; set; }
        public decimal? NetFlow { get; set; }
        public decimal? FlowPercentage { get; set; }
        public decimal? CashPercentage { get; set; }
        public DateTime? Terminated { get; set; }
        public DateTime? ModelChange { get; set; }
        public string? EntityName { get; set; }
        public string? GroupName { get; set; }
        public string? Benchmark { get; set; }
        public bool? NoComposite { get; set; }
        public bool? ToExclusions { get; set; }
        public string? ExclusionType { get; set; }
        public int? NoCompositeType { get; set; }
        public Guid UploadId { get; set; }
    }
}
