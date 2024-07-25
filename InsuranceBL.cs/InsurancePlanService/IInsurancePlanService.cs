using InsuranceAppRLL.Entities;
<<<<<<< HEAD
=======
using InsuranceMLL.InsurancePlanModels;
>>>>>>> 934d4899729eba4fa6d9b04ca3e54115e79f8128
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.InsurancePlanService
{
    public interface IInsurancePlanService
    {
<<<<<<< HEAD
        Task AddPolicy(Policy policy);

        Task UpdatePolicy(Policy policy);

        Task DeletePolicy(int policyId);
=======
        Task CreateInsurancePlanAsync(InsurancePlanCreationModel insurancePlan);

        Task DeleteInsurancePlanAsync(int planId);

        Task<IEnumerable<InsurancePlan>> GetAllInsurancePlansAsync();

        Task<InsurancePlan> GetInsurancePlanByIdAsync(int planId);

        Task UpdateInsurancePlanAsync(UpdateInsurancePlanModel insurancePlan, int planId);
>>>>>>> 934d4899729eba4fa6d9b04ca3e54115e79f8128
    }
}
