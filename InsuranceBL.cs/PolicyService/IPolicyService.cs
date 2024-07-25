using InsuranceAppRLL.Entities;
using InsuranceMLL.PolicyModels;
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

        Task CreatePolicyAsync(PolicyRegistrationModel policy);
        Task UpdatePolicyAsync(PolicyUpdateModel policy, int policyId);
        Task DeletePolicyAsync(int policyId);
    }
}
