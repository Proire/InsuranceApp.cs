using InsuranceAppRLL.CQRS.Commands.InsurancePlanCommands;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsurancePlanRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.InsurancePlanHandlers
{
    public class InsertInsurancePlanCommandHandler : IRequestHandler<InsertInsurancePlanCommand, InsurancePlan>
    {
        private readonly IInsurancePlanCommandRepository _insurancePlanCommandRepository;

        public InsertInsurancePlanCommandHandler(IInsurancePlanCommandRepository insurancePlanCommandRepository)
        {
            _insurancePlanCommandRepository = insurancePlanCommandRepository;
        }

        public async Task<InsurancePlan> Handle(InsertInsurancePlanCommand request, CancellationToken cancellationToken)
        {
            var insurancePlan = new InsurancePlan
            {
                PlanName = request.PlanName,
                PlanDetails = request.PlanDetails,
                CreatedAt = DateTime.UtcNow
            };

            await _insurancePlanCommandRepository.AddPlan(insurancePlan);
            return insurancePlan;
        }
    }
}
