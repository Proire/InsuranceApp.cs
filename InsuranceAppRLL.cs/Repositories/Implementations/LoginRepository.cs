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

        public async Task<string> LoginAdminAsync(LoginModel model)
        {
            // Check if the admin exists asynchronously
            bool isAdmin = await _context.Admins.AnyAsync(x => x.Email == model.Email);

            if (isAdmin)
            {
                // Retrieve the admin asynchronously
                Admin? admin = await _context.Admins.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (admin != null)
                {
                    // Retrieve the key and IV for the admin
                    (byte[] key, byte[] iv) = KeyIvManager.GetKeyAndIv(admin.Email);

                    // Verify the password
                    byte[] cipheredPassword = Convert.FromBase64String(admin.Password);
                    string decryptedPassword = PasswordHasher.VerifyPassword(cipheredPassword, key, iv);

                    if (model.Password == decryptedPassword)
                    {
                        // Generate a token for the admin
                        string token = _jwtTokenGenerator.GenerateAdminToken(Convert.ToString(admin.AdminID), admin.Username, TimeSpan.FromMinutes(15));

                        return token;
                    }

                    throw new LoginException("Wrong Password, Reenter Password");
                }

                throw new LoginException("Invalid Email, Register First");
            }

            throw new LoginException("Invalid Email, Register First");
        }

        public async Task<string> LoginCustomerAsync(LoginModel model)
        {
            // Check if the customer exists asynchronously
            bool isCustomer = await _context.Customers.AnyAsync(x => x.Email == model.Email);

            if (isCustomer)
            {
                // Retrieve the customer asynchronously
                Customer? customer = await _context.Customers.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (customer != null)
                {
                    // Retrieve the key and IV for the customer
                    (byte[] key, byte[] iv) = KeyIvManager.GetKeyAndIv(customer.Email);

                    // Verify the password
                    byte[] cipheredPassword = Convert.FromBase64String(customer.Password);
                    string decryptedPassword = PasswordHasher.VerifyPassword(cipheredPassword, key, iv);

                    if (model.Password == decryptedPassword)
                    {
                        // Generate a token for the customer
                        string token = _jwtTokenGenerator.GenerateCustomerToken(Convert.ToString(customer.CustomerID), customer.FullName, TimeSpan.FromMinutes(15));

                        return token;
                    }

                    throw new LoginException("Wrong Password, Reenter Password");
                }

                throw new LoginException("Invalid Email, Register First");
            }

            throw new LoginException("Invalid Email, Register First");
        }

        public async Task<string> LoginInsuranceAgentAsync(LoginModel model)
        {
            // Check if the insurance agent exists asynchronously
            bool isAgent = await _context.InsuranceAgents.AnyAsync(x => x.Email == model.Email);

            if (isAgent)
            {
                // Retrieve the insurance agent asynchronously
                InsuranceAgent? agent = await _context.InsuranceAgents.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (agent != null)
                {
                    // Retrieve the key and IV for the insurance agent
                    (byte[] key, byte[] iv) = KeyIvManager.GetKeyAndIv(agent.Email);

                    // Verify the password
                    byte[] cipheredPassword = Convert.FromBase64String(agent.Password);
                    string decryptedPassword = PasswordHasher.VerifyPassword(cipheredPassword, key, iv);

                    if (model.Password == decryptedPassword)
                    {
                        // Generate a token for the insurance agent
                        string token = _jwtTokenGenerator.GenerateInsuranceAgentToken(Convert.ToString(agent.AgentID), agent.Username, TimeSpan.FromMinutes(15));

                        return token;
                    }

                    throw new LoginException("Wrong Password, Reenter Password");
                }

                throw new LoginException("Invalid Email, Register First");
            }

            throw new LoginException("Invalid Email, Register First");
        }

        public async Task<string> LoginEmployeeAsync(LoginModel model)
        {
            // Check if the employee exists asynchronously
            bool isEmployee = await _context.Employees.AnyAsync(x => x.Email == model.Email);

            if (isEmployee)
            {
                // Retrieve the employee asynchronously
                Employee? employee = await _context.Employees.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (employee != null)
                {
                    // Retrieve the key and IV for the employee
                    (byte[] key, byte[] iv) = KeyIvManager.GetKeyAndIv(employee.Email);

                    // Verify the password
                    byte[] cipheredPassword = Convert.FromBase64String(employee.Password);
                    string decryptedPassword = PasswordHasher.VerifyPassword(cipheredPassword, key, iv);

                    if (model.Password == decryptedPassword)
                    {
                        // Generate a token for the employee
                        string token = _jwtTokenGenerator.GenerateEmployeeToken(Convert.ToString(employee.EmployeeID), employee.Username, TimeSpan.FromMinutes(15));

                        return token;
                    }

                    throw new LoginException("Wrong Password, Reenter Password");
                }

                throw new LoginException("Invalid Email, Register First");
            }

            throw new LoginException("Invalid Email, Register First");
        }
    }
}
