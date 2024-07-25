using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.PolicyRepository
{
    public interface IPolicyCommandRepository
    {
        Task AddPolicy(Policy policy);

        Task UpdatePolicy(Policy policy);

        Task DeletePolicy(int policyId);
    }
}
