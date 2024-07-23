using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries
{
    public class LoginQuery : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }   

        public LoginQuery(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
