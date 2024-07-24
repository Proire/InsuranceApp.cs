using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces;
using InsuranceMLL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRLL.Utilities;

namespace InsuranceAppRLL.Repositories.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        private readonly InsuranceDbContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public LoginRepository(InsuranceDbContext context, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> LoginAsync(LoginModel model)
        {
            dynamic user = null;

            switch (model.Role.ToLower())
            {
                case "employee":
                    user = await _context.Employees.FirstOrDefaultAsync(x => x.Email == model.Email);
                    break;
                case "admin":
                    user = await _context.Admins.FirstOrDefaultAsync(x => x.Email == model.Email);
                    break;
                case "customer":
                    user = await _context.Customers.FirstOrDefaultAsync(x => x.Email == model.Email);
                    break;
                case "agent":
                    user = await _context.InsuranceAgents.FirstOrDefaultAsync(x => x.Email == model.Email);
                    break;
                default:
                    throw new LoginException("Invalid Role");
            }

            if (user == null)
            {
                throw new LoginException("Invalid Email, Register First");
            }


            string email = user.Email;
            string password = user.Password;
            string id = user switch
            {
                Employee e => e.EmployeeID.ToString(),
                Admin a => a.AdminID.ToString(),
                Customer c => c.CustomerID.ToString(),
                InsuranceAgent ia => ia.AgentID.ToString(),
                _ => throw new LoginException("Invalid Role")
            };

            string username = user.Username;
            (byte[] key, byte[] iv) = KeyIvManager.GetKeyAndIv(email);
            byte[] cipheredPassword = Convert.FromBase64String(password);
            string decryptedPassword = PasswordHasher.VerifyPassword(cipheredPassword, key, iv);

            if (model.Password != decryptedPassword)
            {
                throw new LoginException("Wrong Password, Reenter Password");
            }

            string token = model.Role.ToLower() switch
            {
                "employee" => _jwtTokenGenerator.GenerateEmployeeToken(id, username, TimeSpan.FromMinutes(15)),
                "admin" => _jwtTokenGenerator.GenerateAdminToken(id, username, TimeSpan.FromMinutes(15)),
                "customer" => _jwtTokenGenerator.GenerateCustomerToken(id, user.FullName, TimeSpan.FromMinutes(15)),
                "agent" => _jwtTokenGenerator.GenerateInsuranceAgentToken(id, username, TimeSpan.FromMinutes(15)),
                _ => throw new LoginException("Invalid Role")
            };

            return token;
        }

    }
}
