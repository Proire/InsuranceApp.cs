using InsuranceAppRLL.CQRS.Commands.InsurancePlanCommands;
using InsuranceAppRLL.CQRS.Queries.InsurancePlanQueries;
using InsuranceAppRLL.Entities;
using InsuranceMLL.InsurancePlanModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.InsurancePlanService
{
    public class InsurancePlanService : IInsurancePlanService
    {
        private readonly IMediator _mediator;

        public InsurancePlanService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateInsurancePlanAsync(InsurancePlanCreationModel insurancePlan)
        {
            var command = new InsertInsurancePlanCommand(insurancePlan.PlanName, insurancePlan.PlanDetails);
            await _mediator.Send(command);
        }

        public async Task DeleteInsurancePlanAsync(int planId)
        {
            await _mediator.Send(new DeleteInsurancePlanCommand(planId));
        }

        public async Task<IEnumerable<InsurancePlan>> GetAllInsurancePlansAsync()
        {
            return await _mediator.Send(new GetAllInsurancePlansQuery());
        }

        public async Task<InsurancePlan> GetInsurancePlanByIdAsync(int planId)
        {
            return await _mediator.Send(new GetInsurancePlanByIdQuery(planId));
        }
    }
}
