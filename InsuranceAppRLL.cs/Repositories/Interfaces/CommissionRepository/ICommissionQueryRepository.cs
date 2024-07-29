using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.CommissionRepository
{
    public interface ICommissionQueryRepository
    {
        public Task<double> GetTotalCommissionForAgentAsync(int agentId);
        //public Task<double> GetCommissionForAgentByCustomer(int agentId, int customerId);
    }
}
