﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FOP.Core.Entities
{
    [Table("GrossPerformanceBatch", Schema = "dbo")]
    public class GrossPerformanceBatchModel
    {
        [Key] public int ID { get; set; }
        public int EntityID { get; set; }
        public string? EntityName { get; set; }
        public string? GroupName { get; set; }
        public string? Benchmark { get; set; }
        [Column(TypeName = "decimal(32, 18)")]
        public decimal? PeriodBeginningMarketValue { get; set; }
        [Column(TypeName = "decimal(32, 18)")]
        public decimal? PeriodEndingMarketValue { get; set; }
        public string PeriodPerformance { get; set; }
        public string? EntityPath { get; set; }
        public Guid UploadID { get; set; }
    }
}
