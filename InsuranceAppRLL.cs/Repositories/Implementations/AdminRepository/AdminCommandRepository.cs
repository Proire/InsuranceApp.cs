using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using InsuranceAppRLL.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserModelLayer;
using UserRLL.Utilities;

namespace InsuranceAppRLL.Repositories.Implementations.AdminRepository
{
    public class AdminCommandRepository : IAdminCommandRepository
    {
        private readonly InsuranceDbContext _context;
        private readonly RabitMQProducer _rabbitMqService;

        public AdminCommandRepository(InsuranceDbContext context, RabitMQProducer rabbitMqService)
        {
            _context = context;
            _rabbitMqService = rabbitMqService;
        }

        public async Task RegisterAdminAsync(Admin admin)
        {
            string password = admin.Password;
            // Generate a unique key and IV for the admin
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                byte[] key = aes.Key;
                byte[] iv = aes.IV;

                // Store the key and IV in a file or secure storage
                KeyIvManager.SaveKeyAndIv(admin.Email, key, iv);


                // Hash the admin's password using the generated key and IV
                admin.Password = PasswordHasher.HashPassword(admin.Password, key, iv);
            }

            try
            {
                await _context.Admins.AddAsync(admin);
                await _context.SaveChangesAsync();

                // Send confirmation email with credentials using RabbitMQ
                var emailDto = new EmailDTO
                {
                    To = admin.Email,
                    Subject = "Admin Registration Confirmation",
                    Body = $"Dear {admin.FullName},\n\nYour admin account has been successfully created.\n\nYour login credentials are:\nEmail: {admin.Email}\nPassword: {password}.\n\nBest regards,\nInsuranceApp Team"
                };

                string message = JsonSerializer.Serialize(emailDto);
                _rabbitMqService.SendMessage(message);
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new AdminException("An error occurred while registering the admin.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new AdminException("An unexpected error occurred.", ex);
            }
        }
    }
}
