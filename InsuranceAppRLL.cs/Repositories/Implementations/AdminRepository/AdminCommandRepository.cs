using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using InsuranceAppRLL.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public async Task DeleteAdminAsync(int adminId)
        {
            try
            {
                var admin = await _context.Admins.FindAsync(adminId);
                if (admin != null)
                {
                    _context.Admins.Remove(admin);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new AdminException($"No admin found with id: {adminId}");
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new AdminException("An error occurred while deleting the admin from the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new AdminException("An unexpected error occurred.", ex);
            }
        }


        public async Task RegisterAdminAsync(Admin admin)
        {

            try
            {
                // Check if an admin with the same email already exists
                if (await _context.Admins.AnyAsync(a => a.Email == admin.Email))
                {
                    throw new AdminException("An admin with this email already exists.");
                }

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

        public async Task UpdateAdminAsync(Admin admin)
        {
            try
            {
                // Check if an admin with the same email already exists
                if (await _context.Admins.AnyAsync(a => a.Email == admin.Email))
                {
                    throw new AdminException("An admin with this email already exists.");
                }

                var existingAdmin = await _context.Admins.FindAsync(admin.AdminID);
                if (existingAdmin == null)
                {
                    throw new AdminException($"No admin found with id: {admin.AdminID}");
                }

                existingAdmin.Username = admin.Username;
                existingAdmin.FullName = admin.FullName;

                if(existingAdmin.Email != admin.Email)
                {
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
                        existingAdmin.Password = PasswordHasher.HashPassword(admin.Password, key, iv);
                        existingAdmin.Email = admin.Email;  
                    }

                    // Send confirmation email with credentials using RabbitMQ
                    var emailDto = new EmailDTO
                    {
                        To = admin.Email,
                        Subject = "Admin Registration Confirmation",
                        Body = $"Dear {admin.FullName},\n\nYour admin account has been successfully Verified .\n\nYour login credentials are:\nEmail: {admin.Email}\nPassword: {admin.Password}.\n\nBest regards,\nInsuranceApp Team"
                    };

                    string message = JsonSerializer.Serialize(emailDto);
                    _rabbitMqService.SendMessage(message);
                }

                if (existingAdmin.Password != admin.Password)
                {

                    // Generate a unique key and IV for the user
                    using (Aes aes = Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;


                        // Store the key and IV in a file
                        KeyIvManager.UpdateKeyAndIv(admin.Email, key, iv);

                        // Hash the user's password using the generated key and IV
                        existingAdmin.Password = PasswordHasher.HashPassword(admin.Password, key, iv);

                    }
                }

                _context.Admins.Update(existingAdmin);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflicts
                throw new AdminException("A concurrency conflict occurred while updating the admin.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new AdminException("An error occurred while updating the admin in the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new AdminException("An unexpected error occurred.", ex);
            }
        }

    }
}
