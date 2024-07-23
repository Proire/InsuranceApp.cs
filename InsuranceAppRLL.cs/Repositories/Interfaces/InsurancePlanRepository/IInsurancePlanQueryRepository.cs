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
        public Task<List<InsurancePlan>> getAllInsurancePlans();
        public Task<InsurancePlan> getInsurancePlan(int planId);
    }
}
