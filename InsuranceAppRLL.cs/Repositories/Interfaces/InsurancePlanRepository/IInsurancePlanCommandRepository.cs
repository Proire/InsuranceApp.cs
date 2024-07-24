using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.InsurancePlanRepository
{
    public interface IInsurancePlanCommandRepository
    {
        public Task AddPlan(InsurancePlan plan); 
    }
}
