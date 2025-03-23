using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Models
{
    public class DynamicLimitsModel
    {
        public decimal MinFlowPercentage { get; set; }
        public decimal MaxFlowPercentage { get; set; }
        public decimal CashUMAPercentage { get; set; }
        public decimal CashOverPercentage { get; set; }
        public decimal CashOverDestPercentage { get; set; }
        public int EntityTestValue { get; set; }
    }
}
