using InsuranceAppRLL.Entities;
using InsuranceMLL.InsuranceAgentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.InsuranceAgentService
{
    public interface IInsuranceAgentService
    {
        Task RegisterInsuranceAgentAsync(InsuranceAgentRegistrationModel insuranceAgent);

        Task<IEnumerable<InsuranceAgent>> GetAllInsuranceAgents();

        Task<InsuranceAgent> GetInsuranceAgentAsync(int id);
    }
}
