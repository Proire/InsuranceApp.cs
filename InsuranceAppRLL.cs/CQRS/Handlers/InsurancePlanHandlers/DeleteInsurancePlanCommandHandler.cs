using InsuranceAppRLL.CQRS.Commands.InsurancePlanCommands;
using InsuranceAppRLL.Repositories.Interfaces.InsurancePlanRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.InsurancePlanHandlers
{
    public class DeleteInsurancePlanCommandHandler : IRequestHandler<DeleteInsurancePlanCommand>
    {
        private readonly IInsurancePlanCommandRepository _insurancePlanCommandRepository;

        public DeleteInsurancePlanCommandHandler(IInsurancePlanCommandRepository insurancePlanCommandRepository)
        {
            _insurancePlanCommandRepository = insurancePlanCommandRepository;
        }

        public async Task<Unit> Handle(DeleteInsurancePlanCommand request, CancellationToken cancellationToken)
        {
            await _insurancePlanCommandRepository.DeletePlan(request.PlanId);
            return Unit.Value;
        }
    }
}
