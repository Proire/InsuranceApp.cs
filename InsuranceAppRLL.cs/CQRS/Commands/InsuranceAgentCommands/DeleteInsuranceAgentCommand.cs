using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.InsuranceAgentCommands
{
    public class DeleteInsuranceAgentCommand : IRequest
    {
        public int AgentId { get; set; }

        public DeleteInsuranceAgentCommand(int agentId)
        {
            AgentId = agentId;
        }
    }
}
