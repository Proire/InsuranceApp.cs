using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeRepository;
using InsuranceAppRLL.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserModelLayer;

namespace InsuranceAppRLL.Repositories.Implementations.EmployeeRepository
{
    public class EmployeeCommandRepository : IEmployeeCommandRepository
    {
        private readonly InsuranceDbContext _context;
        private readonly RabitMQProducer _rabbitMqService;

        public EmployeeCommandRepository(InsuranceDbContext context, RabitMQProducer rabbitMqService)
        {
            _context = context;
            _rabbitMqService = rabbitMqService;
        }

        public async Task RegisterEmployeeAsync(Employee employee)
        {
            string password = employee.Password;
            // Generate a unique key and IV for the employee
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                byte[] key = aes.Key;
                byte[] iv = aes.IV;

                // Store the key and IV in a file or secure storage
                KeyIvManager.SaveKeyAndIv(employee.Email, key, iv);

                // Hash the employee's password using the generated key and IV
                employee.Password = PasswordHasher.HashPassword(employee.Password, key, iv);
            }

            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                // Send confirmation email with credentials using RabbitMQ
                var emailDto = new EmailDTO
                {
                    To = employee.Email,
                    Subject = "Employee Registration Confirmation",
                    Body = $"Dear {employee.FullName},\n\nYour employee account has been successfully created.\n\nYour login credentials are:\nEmail: {employee.Email}\nPassword: {password}.\n\nBest regards,\nInsuranceApp Team"
                };

                string message = JsonSerializer.Serialize(emailDto);
                _rabbitMqService.SendMessage(message);
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new EmployeeException("An error occurred while registering the employee.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new EmployeeException("An unexpected error occurred.", ex);
            }
        }
    }
}
