using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeRepository;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeSchemeRepository;
using InsuranceAppRLL.Utilities;
using InsuranceAppRLL;
using Microsoft.Data.SqlClient;
using UserModelLayer;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class EmployeeCommandRepository : IEmployeeCommandRepository
{
    private readonly InsuranceDbContext _context;
    private readonly RabitMQProducer _rabbitMqService;
    private readonly IEmployeeSchemeCommandRepository _employeeSchemeCommand;

    public EmployeeCommandRepository(InsuranceDbContext context, RabitMQProducer rabbitMqService, IEmployeeSchemeCommandRepository employeeSchemeCommand)
    {
        _context = context;
        _rabbitMqService = rabbitMqService;
        _employeeSchemeCommand = employeeSchemeCommand;
    }

    public async Task RegisterEmployeeAsync(Employee employee)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                if (await _context.Employees.AnyAsync(e => e.Email == employee.Email))
                {
                    throw new EmployeeException("An employee with this email already exists.");
                }

                string password = employee.Password;
                using (var aes = System.Security.Cryptography.Aes.Create())
                {
                    aes.GenerateKey();
                    aes.GenerateIV();
                    byte[] key = aes.Key;
                    byte[] iv = aes.IV;

                    KeyIvManager.SaveKeyAndIv(employee.Email, key, iv);
                    employee.Password = PasswordHasher.HashPassword(employee.Password, key, iv);
                }

                await _context.RegisterEmployeeAsync(employee);
                await transaction.CommitAsync();

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
                await transaction.RollbackAsync();
                KeyIvManager.DeleteKeyAndIv(employee.Email);
                throw new EmployeeException("An error occurred while registering the employee.", ex);
            }
        }
    }

    public async Task DeleteEmployeeAsync(int employeeId)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var employee = await _context.Employees.FindAsync(employeeId);
                if (employee != null)
                {
                    await _context.DeleteEmployeeAsync(employeeId);
                    await _employeeSchemeCommand.DeleteEmployeeFromScheme(employeeId);
                    await transaction.CommitAsync();
                    KeyIvManager.DeleteKeyAndIv(employee.Email);
                }
                else
                {
                    throw new EmployeeException($"No employee found with id: {employeeId}");
                }
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                throw new EmployeeException("An error occurred while deleting the employee from the database.", ex);
            }
        }
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
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
                    using (var aes = Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;

                        KeyIvManager.SaveKeyAndIv(employee.Email, key, iv);
                        existingEmployee.Password = PasswordHasher.HashPassword(employee.Password, key, iv);
                        existingEmployee.Email = employee.Email;
                    }

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
                    using (Aes aes = Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;

                        KeyIvManager.UpdateKeyAndIv(employee.Email, key, iv);
                        existingEmployee.Password = PasswordHasher.HashPassword(employee.Password, key, iv);
                    }
                }

                await _context.UpdateEmployeeAsync(existingEmployee);
                await transaction.CommitAsync();
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                throw new EmployeeException("An error occurred while updating the employee in the database.", ex);
            }
        }
    }
}
