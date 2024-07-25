using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.InsuranceAgentCommands
{
    public class UpdateInsuranceAgentCommand : IRequest
    {
        public int AgentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public UpdateInsuranceAgentCommand(string username, string password, string email, string fullName, int agentId)
        {
            Username = username;
            Password = password;
            Email = email;
            FullName = fullName;
            AgentId = agentId;
        }
    }
}
