<<<<<<< HEAD
﻿using InsuranceAppRLL.Entities;
=======
﻿using InsuranceAppRLL.CQRS.Commands.InsurancePlanCommands;
using InsuranceAppRLL.CQRS.Queries.InsurancePlanQueries;
using InsuranceAppRLL.Entities;
using InsuranceMLL.InsurancePlanModels;
using MediatR;
>>>>>>> 934d4899729eba4fa6d9b04ca3e54115e79f8128
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.InsurancePlanService
{
    public class InsurancePlanService : IInsurancePlanService
    {
<<<<<<< HEAD
        Task AddScheme(Scheme scheme);
        Task UpdateScheme(Scheme scheme);
        Task DeleteScheme(int schemeId);
=======
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

        public async Task UpdateInsurancePlanAsync(UpdateInsurancePlanModel insurancePlan, int planId)
        {
            var UpdateInsurancePlanCommand = new UpdateInsurancePlanCommand(planId, insurancePlan.PlanName,insurancePlan.PlanDetails);
            await _mediator.Send(UpdateInsurancePlanCommand);
        }
>>>>>>> 934d4899729eba4fa6d9b04ca3e54115e79f8128
    }
}
