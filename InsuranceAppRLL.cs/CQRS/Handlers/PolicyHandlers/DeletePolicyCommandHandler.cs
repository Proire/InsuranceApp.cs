using InsuranceAppRLL.CQRS.Commands.PolicyCommands;
using InsuranceAppRLL.Repositories.Interfaces.PolicyRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.PolicyHandlers
{
    public class DeletePolicyCommandHandler : IRequestHandler<DeletePolicyCommand>
    {
        private readonly IPolicyCommandRepository _policyCommandRepository;

        public DeletePolicyCommandHandler(IPolicyCommandRepository policyCommandRepository)
        {
            _policyCommandRepository = policyCommandRepository;
        }

        public async Task<Unit> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
        {
            await _policyCommandRepository.DeletePolicy(request.PolicyID);
            return Unit.Value;
        }
    }
}
