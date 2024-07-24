using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.CustomerCommands
{
    public class UpdateCustomerCommand : IRequest
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int AgentId { get; set; }    

        public UpdateCustomerCommand(int customerId, string fullName, string email, string password, string phone, DateTime dateOfBirth, int agentId)
        {
            CustomerId = customerId;
            FullName = fullName;
            Email = email;
            Password = password;
            Phone = phone;
            DateOfBirth = dateOfBirth;
            AgentId = agentId;
        }
    }
}
