using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.CommissionService
{
    public interface ICommissionService
    {
        Task<double> GetTotalCommissionForAgentAsync(int agentId);
    }
}
