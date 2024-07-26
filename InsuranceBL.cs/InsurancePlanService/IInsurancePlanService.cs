using InsuranceAppRLL.Entities;
using InsuranceMLL.InsurancePlanModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.InsurancePlanService
{
    public interface IInsurancePlanService
    {
        Task CreateInsurancePlanAsync(InsurancePlanCreationModel insurancePlan);

        Task DeleteInsurancePlanAsync(int planId);

        Task<IEnumerable<InsurancePlan>> GetAllInsurancePlansAsync();

        Task<InsurancePlan> GetInsurancePlanByIdAsync(int planId);

        Task UpdateInsurancePlanAsync(UpdateInsurancePlanModel insurancePlan, int planId);
    }
}
