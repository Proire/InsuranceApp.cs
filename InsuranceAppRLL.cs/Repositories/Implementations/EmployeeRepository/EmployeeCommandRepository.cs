using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeRepository;
using InsuranceAppRLL.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            try
            {
                if (await _context.Employees.AnyAsync(e => e.Email == employee.Email))
                {
                    throw new EmployeeException("An employee with this email already exists.");
                }


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
            catch (SqlException ex)
            {
                // Handle specific database update exceptions
                throw new EmployeeException("An error occurred while registering the employee.", ex);
            }
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(employeeId);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                    KeyIvManager.DeleteKeyAndIv(employee.Email);
                }
                else
                {
                    throw new EmployeeException($"No employee found with id: {employeeId}");
                }
            }
            catch (SqlException ex)
            {
                // Handle specific database update exceptions
                throw new EmployeeException("An error occurred while deleting the employee from the database.", ex);
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {

                var existingEmployee = await _context.Employees.FindAsync(employee.EmployeeID);
                if (existingEmployee == null)
                {
                    throw new EmployeeException($"No employee found with id: {employee.EmployeeID}");
                }
                if (await _context.Employees.AnyAsync(e => e.EmployeeID != employee.EmployeeID && e.Email == employee.Email))
                {
                    throw new EmployeeException("An employee with this email already exists.");
                }

                existingEmployee.Username = employee.Username;
                existingEmployee.FullName = employee.FullName;

                if (existingEmployee.Email != employee.Email)
                {
                    // Generate a unique key and IV for the employee
                    using (var aes = Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;

                        // Store the key and IV in a file or secure storage
                        KeyIvManager.SaveKeyAndIv(employee.Email, key, iv);

                        // Hash the employee's password using the generated key and IV
                        existingEmployee.Password = PasswordHasher.HashPassword(employee.Password, key, iv);
                        existingEmployee.Email = employee.Email;
                    }

                    // Send confirmation email with credentials using RabbitMQ
                    var emailDto = new EmailDTO
                    {
                        To = employee.Email,
                        Subject = "Employee Registration Confirmation",
                        Body = $"Dear {employee.FullName},\n\nYour employee account has been successfully created.\n\nYour login credentials are:\nEmail: {employee.Email}\nPassword: {employee.Password}.\n\nBest regards,\nInsuranceApp Team"
                    };

                    string message = JsonSerializer.Serialize(emailDto);
                    _rabbitMqService.SendMessage(message);
                }

                if (existingEmployee.Password != employee.Password)
                {
                    // Generate a unique key and IV for the employee
                    using (Aes aes = Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;

                        // Store the key and IV in a file
                        KeyIvManager.UpdateKeyAndIv(employee.Email, key, iv);

                        // Hash the employee's password using the generated key and IV
                        existingEmployee.Password = PasswordHasher.HashPassword(employee.Password, key, iv);
                    }
                }

                _context.Employees.Update(existingEmployee);
                await _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                // Handle specific database update exceptions
                throw new EmployeeException("An error occurred while updating the employee in the database.", ex);
            }
        }
    }
}
