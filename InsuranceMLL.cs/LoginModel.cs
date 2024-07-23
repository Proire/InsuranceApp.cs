using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMLL
{
    public class LoginModel
    {
        public LoginModel(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
