using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMLL.CommissionModels
{
    public class CommissionModel
    {
        public int? AgentID { get; set; } = 0;
        public int PolicyID { get; set; }
        public double Amount { get; set; }
    }
}
