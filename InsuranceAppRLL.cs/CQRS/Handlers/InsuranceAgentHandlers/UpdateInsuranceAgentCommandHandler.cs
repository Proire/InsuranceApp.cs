using InsuranceAppRLL.CQRS.Commands.InsuranceAgentCommands;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsuranceAgentRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.InsuranceAgentHandlers
{
    public class UpdateInsuranceAgentCommandHandler : IRequestHandler<UpdateInsuranceAgentCommand>
    {
        private readonly IInsuranceAgentCommandRepository _agentCommandRepository;

        public UpdateInsuranceAgentCommandHandler(IInsuranceAgentCommandRepository agentCommandRepository)
        {
            _agentCommandRepository = agentCommandRepository;
        }

        public async Task<Unit> Handle(UpdateInsuranceAgentCommand request, CancellationToken cancellationToken)
        {
            InsuranceAgent agent = new InsuranceAgent()
            {
                AgentID = request.AgentId,
                Username = request.Username,
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password
            };

            await _agentCommandRepository.UpdateAgentAsync(agent);
            return Unit.Value;
        }
    }
}
