using InsuranceAppRLL.CQRS.Commands.InsuranceAgentCommands;
using InsuranceAppRLL.Repositories.Interfaces.InsuranceAgentRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.InsuranceAgentHandlers
{
    public class DeleteInsuranceAgentCommandHandler : IRequestHandler<DeleteInsuranceAgentCommand>
    {
        private readonly IInsuranceAgentCommandRepository _agentCommandRepository;

        public DeleteInsuranceAgentCommandHandler(IInsuranceAgentCommandRepository agentCommandRepository)
        {
            _agentCommandRepository = agentCommandRepository;
        }

        public async Task<Unit> Handle(DeleteInsuranceAgentCommand request, CancellationToken cancellationToken)
        {
            await _agentCommandRepository.DeleteAgentAsync(request.AgentId);
            return Unit.Value;
        }
    }
}
