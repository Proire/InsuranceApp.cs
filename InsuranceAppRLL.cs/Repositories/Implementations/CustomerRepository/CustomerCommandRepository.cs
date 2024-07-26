using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.CustomerRepository;
using InsuranceAppRLL.Utilities;
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
            try
            {
                if (await _context.Customers.AnyAsync(c => c.Email == customer.Email))
                {
                    throw new CustomerException("A customer with this email already exists.");
                }

                string password = customer.Password;
                // Generate a unique key and IV for the customer
                using (var aes = System.Security.Cryptography.Aes.Create())
                {
                    aes.GenerateKey();
                    aes.GenerateIV();
                    byte[] key = aes.Key;
                    byte[] iv = aes.IV;

                    // Store the key and IV in a file or secure storage
                    KeyIvManager.SaveKeyAndIv(customer.Email, key, iv);

                    // Hash the customer's password using the generated key and IV
                    customer.Password = PasswordHasher.HashPassword(customer.Password, key, iv);
                }

                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                // Send confirmation email with credentials using RabbitMQ
                var emailDto = new EmailDTO
                {
                    To = customer.Email,
                    Subject = "Customer Registration Confirmation",
                    Body = $"Dear {customer.FullName},\n\nYour customer account has been successfully created.\n\nYour login credentials are:\nEmail: {customer.Email}\nPassword: {password}.\n\nBest regards,\nInsuranceApp Team"
                };

                string message = JsonSerializer.Serialize(emailDto);
                _rabbitMqService.SendMessage(message);
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new CustomerException("An error occurred while registering the customer.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new CustomerException("An unexpected error occurred.", ex);
            }
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            try
            {
                if (await _context.Customers.AnyAsync(c => c.Email == customer.Email))
                {
                    throw new CustomerException("A customer with this email already exists.");
                }

                // Find the existing customer
                var existingCustomer = await _context.Customers.FindAsync(customer.CustomerID);
                if (existingCustomer == null)
                {
                    throw new CustomerException($"No customer found with id: {customer.CustomerID}");
                }

                // Update customer details
                existingCustomer.FullName = customer.FullName;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.DateOfBirth = customer.DateOfBirth;

                // Check if email has changed
                if (existingCustomer.Email != customer.Email)
                {
                    // Generate and store new key and IV
                    using (var aes = System.Security.Cryptography.Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;

                        KeyIvManager.SaveKeyAndIv(customer.Email, key, iv);

                        // Hash the new password
                        existingCustomer.Password = PasswordHasher.HashPassword(customer.Password, key, iv);
                        existingCustomer.Email = customer.Email;
                    }

                    // Send confirmation email with credentials using RabbitMQ
                    var emailDto = new EmailDTO
                    {
                        To = customer.Email,
                        Subject = "Admin Registration Confirmation",
                        Body = $"Dear {customer.FullName},\n\nYour admin account has been successfully created.\n\nYour login credentials are:\nEmail: {customer.Email}\nPassword: {customer.Password}.\n\nBest regards,\nInsuranceApp Team"
                    };

                    string message = JsonSerializer.Serialize(emailDto);
                    _rabbitMqService.SendMessage(message);
                }
                else if (existingCustomer.Password != customer.Password)
                {
                    // Generate and store new key and IV for password change
                    using (var aes = System.Security.Cryptography.Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;

                        KeyIvManager.UpdateKeyAndIv(customer.Email, key, iv);

                        // Hash the new password
                        existingCustomer.Password = PasswordHasher.HashPassword(customer.Password, key, iv);
                    }
                }

                // Update customer in the context
                _context.Customers.Update(existingCustomer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflicts
                throw new CustomerException("A concurrency conflict occurred while updating the customer.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new CustomerException("An error occurred while updating the customer in the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new CustomerException("An unexpected error occurred.", ex);
            }
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            try
            {
                // Find the existing customer
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer != null)
                {
                    // Remove the customer
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new CustomerException($"No customer found with id: {customerId}");
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new CustomerException("An error occurred while deleting the customer from the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new CustomerException("An unexpected error occurred.", ex);
            }
        }
    }
}
