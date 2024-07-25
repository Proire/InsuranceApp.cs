using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.InsurancePlanRepository
{
    public interface IInsurancePlanQueryRepository
    {
        public Task<List<InsurancePlan>> GetAllInsurancePlans();
        public Task<InsurancePlan> GetInsurancePlan(int planId);
    }
}
