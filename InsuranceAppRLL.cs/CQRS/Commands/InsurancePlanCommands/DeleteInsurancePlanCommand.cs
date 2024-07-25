using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.InsurancePlanCommands
{
    public class DeleteInsurancePlanCommand : IRequest
    {
        public int PlanId { get; set; }

        public DeleteInsurancePlanCommand(int planId)
        {
            PlanId = planId;
        }
    }
}
