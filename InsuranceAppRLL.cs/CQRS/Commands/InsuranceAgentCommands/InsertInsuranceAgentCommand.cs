using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.InsuranceAgentCommands
{
    public class InsertInsuranceAgentCommand : IRequest<InsuranceAgent>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public InsertInsuranceAgentCommand(string username, string password, string email, string fullName)
        {
            Username = username;
            Password = password;
            Email = email;
            FullName = fullName;
        }
    }
}
