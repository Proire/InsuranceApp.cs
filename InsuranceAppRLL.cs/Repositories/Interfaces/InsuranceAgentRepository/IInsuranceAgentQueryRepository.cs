using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.InsuranceAgentRepository
{
    public interface IInsuranceAgentQueryRepository
    {
        Task<InsuranceAgent> GetAgentByIDAsync(int agentId);
        Task<IEnumerable<InsuranceAgent>> GetAllAsync();
    }
}
