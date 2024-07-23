using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModelLayer
{
    public class LoginModel
    {
        public LoginModel(string email, string password)
        {
            Email = email;    
            Password = password;    
        }
        public string Email { get; set; } 
        public string Password { get; set; } 
    }
}
