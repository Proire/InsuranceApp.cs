using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.PolicyService
{
    public interface IPolicyService
    {
        Task<IEnumerable<Policy>> GetAllPoliciesForCustomersAsync(int customerId);

        Task<Policy> GetPolicyByIdAsync(int policyId);
    }
}
