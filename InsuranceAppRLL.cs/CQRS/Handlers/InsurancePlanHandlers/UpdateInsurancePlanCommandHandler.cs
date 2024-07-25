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
    public class UpdateInsurancePlanCommandHandler : IRequestHandler<UpdateInsurancePlanCommand>
    {
        private readonly IInsurancePlanCommandRepository _repository;

        public UpdateInsurancePlanCommandHandler(IInsurancePlanCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateInsurancePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = new InsurancePlan
            {
                PlanID = request.PlanID,
                PlanName = request.PlanName,
                PlanDetails = request.PlanDetails
            };

            await _repository.UpdatePlan(plan);
            return Unit.Value;
        }
    }
}
