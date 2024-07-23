using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.CustomerCommands
{
    public class InsertCustomerCommand : IRequest<Customer>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? AgentID { get; set; }

        public InsertCustomerCommand(string fullName, string email, string password, string phone, DateTime dateOfBirth, int? agentID)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Phone = phone;
            DateOfBirth = dateOfBirth;
            AgentID = agentID;
        }
    }
}
