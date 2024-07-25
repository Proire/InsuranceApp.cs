using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.InsurancePlanCommands
{
    public class InsertInsurancePlanCommand : IRequest<InsurancePlan>
    {
        public string PlanName { get; set; }
        public string PlanDetails { get; set; }

        public InsertInsurancePlanCommand(string planName, string planDetails)
        {
            PlanName = planName;
            PlanDetails = planDetails;
        }
    }
}
