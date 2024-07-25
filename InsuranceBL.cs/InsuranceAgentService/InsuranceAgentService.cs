using InsuranceAppRLL.CQRS.Commands.InsuranceAgentCommands;
using InsuranceAppRLL.CQRS.Queries.InsuranceAgentQueries;
using InsuranceAppRLL.Entities;
using InsuranceMLL.InsuranceAgentModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.InsuranceAgentService
{
    public class InsuranceAgentService : IInsuranceAgentService
    {
        private readonly IMediator _mediator;

        public InsuranceAgentService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<InsuranceAgent>> GetAllInsuranceAgents()
        {
            return await _mediator.Send(new GetAllInsuranceAgentsQuery());
        }

        public async Task<InsuranceAgent> GetInsuranceAgentAsync(int id)
        {
            return await _mediator.Send(new GetInsuranceAgentByIdQuery(id));
        }

        public async Task RegisterInsuranceAgentAsync(InsuranceAgentRegistrationModel agent)
        {
            var command = new InsertInsuranceAgentCommand(agent.Username, agent.Password, agent.Email, agent.FullName);
            await _mediator.Send(command);
        }
        public async Task DeleteAgentAsync(int agentId)
        {
            await _mediator.Send(new DeleteInsuranceAgentCommand(agentId));
        }

        public async Task UpdateAgentAsync(AgentUpdateModel agent, int agentId)
        {
            await _mediator.Send(new UpdateInsuranceAgentCommand(agent.Username, agent.Password, agent.Email, agent.FullName, agentId));
        }
    }
}
