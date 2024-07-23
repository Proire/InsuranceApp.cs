using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.EmployeeCommands
{
    public class InsertEmployeeCommand : IRequest<Employee>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }

        public InsertEmployeeCommand(string username, string password, string email, string fullName, string role)
        {
            Username = username;
            Password = password;
            Email = email;
            FullName = fullName;
            Role = role;
        }
    }
}
