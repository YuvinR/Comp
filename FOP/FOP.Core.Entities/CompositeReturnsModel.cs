using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    public class CompositeReturnsModel
    {
        public Guid CompositeID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Composite { get; set; }
        public DateTime CompositePeriodEnding { get; set; }
        public float CompositeAssetsEnding { get; set; }
        public float NumberOfAccounts { get; set; }
        public float AnnualGross { get; set; }
        public float AnnualNet { get; set; }
        public float? BenchmarkReturnPercentage { get; set; }
        public float? AnnualDispersion { get; set; }
        public float? TotalFirmAssetsUSD { get; set; } 
        public float? ThreeYearStdDevComposite { get; set; } 
        public float? ThreeYearStdDevBenchmark { get; set; } 
        public string Benchmark { get; set; }
        public DateTime? CompositeCreationDate { get; set; }
        public DateTime? CompositeStartDate { get; set; }
    }
}
