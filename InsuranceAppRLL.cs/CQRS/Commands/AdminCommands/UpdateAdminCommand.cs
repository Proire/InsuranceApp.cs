using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.AdminCommands
{
    public class UpdateAdminCommand : IRequest
    {
        public int AdminId { get; set; }    
        public string Username { get; set; }    

        public string Password { get; set; }    

        public string Email { get; set; }   

        public string FullName { get; set; }    

        public UpdateAdminCommand(string username, string password, string email, string fullName, int adminId)
        {
            Username = username;
            Password = password;
            Email = email;
            FullName = fullName;
            AdminId = adminId;
        }
    }
}
