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

            try
            {
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
    }
}
