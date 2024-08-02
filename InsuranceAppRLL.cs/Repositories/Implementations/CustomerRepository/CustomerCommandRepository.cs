using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.CustomerRepository;
using InsuranceAppRLL.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserModelLayer;

namespace InsuranceAppRLL.Repositories.Implementations.CustomerRepository
{
    public class CustomerCommandRepository : ICustomerCommandRepository
    {
        private readonly InsuranceDbContext _context;
        private readonly RabitMQProducer _rabbitMqService;

        public CustomerCommandRepository(InsuranceDbContext context, RabitMQProducer rabbitMqService)
        {
            _context = context;
            _rabbitMqService = rabbitMqService;
        }

        public async Task RegisterCustomerAsync(Customer customer)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await _context.Customers.AnyAsync(c => c.Email == customer.Email))
                    {
                        throw new CustomerException("A customer with this email already exists.");
                    }

                    string password = customer.Password;
                    using (var aes = System.Security.Cryptography.Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;

                        KeyIvManager.SaveKeyAndIv(customer.Email, key, iv);
                        customer.Password = PasswordHasher.HashPassword(customer.Password, key, iv);
                    }

                    await _context.RegisterCustomerAsync(customer.FullName, customer.Email, customer.Password, customer.Phone, customer.DateOfBirth, customer.AgentID);

                    var emailDto = new EmailDTO
                    {
                        To = customer.Email,
                        Subject = "Customer Registration Confirmation",
                        Body = $"Dear {customer.FullName},\n\nYour customer account has been successfully created.\n\nYour login credentials are:\nEmail: {customer.Email}\nPassword: {password}.\n\nBest regards,\nInsuranceApp Team"
                    };

                    string message = JsonSerializer.Serialize(emailDto);
                    _rabbitMqService.SendMessage(message);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    KeyIvManager.DeleteKeyAndIv(customer.Email);
                    throw new CustomerException("An error occurred while registering the customer.", ex);
                }
            }
        }


        public async Task UpdateCustomerAsync(Customer customer)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingCustomer = await _context.Customers.FindAsync(customer.CustomerID);
                    if (existingCustomer == null)
                    {
                        throw new CustomerException($"Customer Not found");
                    }
                    if (await _context.Customers.AnyAsync(c => c.CustomerID != customer.CustomerID && c.Email == customer.Email))
                    {
                        throw new CustomerException("A customer with this email already exists.");
                    }

                    existingCustomer.FullName = customer.FullName;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.DateOfBirth = customer.DateOfBirth;

                    if (existingCustomer.Email != customer.Email)
                    {
                        using (var aes = System.Security.Cryptography.Aes.Create())
                        {
                            aes.GenerateKey();
                            aes.GenerateIV();
                            byte[] key = aes.Key;
                            byte[] iv = aes.IV;

                            KeyIvManager.SaveKeyAndIv(customer.Email, key, iv);
                            existingCustomer.Password = PasswordHasher.HashPassword(customer.Password, key, iv);
                            existingCustomer.Email = customer.Email;
                        }

                        var emailDto = new EmailDTO
                        {
                            To = customer.Email,
                            Subject = "Customer Registration Confirmation",
                            Body = $"Dear {customer.FullName},\n\nYour customer account has been successfully created.\n\nYour login credentials are:\nEmail: {customer.Email}\nPassword: {customer.Password}.\n\nBest regards,\nInsuranceApp Team"
                        };

                        string message = JsonSerializer.Serialize(emailDto);
                        _rabbitMqService.SendMessage(message);
                    }
                    else if (existingCustomer.Password != customer.Password)
                    {
                        using (var aes = System.Security.Cryptography.Aes.Create())
                        {
                            aes.GenerateKey();
                            aes.GenerateIV();
                            byte[] key = aes.Key;
                            byte[] iv = aes.IV;

                            KeyIvManager.UpdateKeyAndIv(customer.Email, key, iv);
                            existingCustomer.Password = PasswordHasher.HashPassword(customer.Password, key, iv);
                        }
                    }

                    await _context.UpdateCustomerAsync(customer.CustomerID, customer.FullName, customer.Email, customer.Password, customer.Phone, customer.DateOfBirth, existingCustomer.AgentID);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await transaction.RollbackAsync();
                    throw new CustomerException("An error occurred while updating the customer in the database.", ex);
                }
            }
        }


        public async Task DeleteCustomerAsync(int customerId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var customer = await _context.Customers.FindAsync(customerId);
                    if (customer != null)
                    {
                        await _context.DeleteCustomerAsync(customerId);
                        KeyIvManager.DeleteKeyAndIv(customer.Email);
                    }
                    else
                    {
                        throw new CustomerException($"Customer Not found");
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await transaction.RollbackAsync();
                    throw new CustomerException("An error occurred while deleting the customer from the database.", ex);
                }
            }
        }

    }
}
