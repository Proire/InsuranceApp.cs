using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using InsuranceAppRLL.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.Json;
using UserModelLayer;

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
            using var transaction = await _context.Database.BeginTransactionAsync();
            Admin admin = null;

            try
            {
                admin = await _context.Admins.FindAsync(adminId);
                if (admin == null)
                {
                    throw new AdminException("Admin not found");
                }

                // Call the DbContext method to execute the stored procedure for deleting the admin
                await _context.DeleteAdminAsync(adminId);

                // Remove the admin entity from the context
                _context.Admins.Remove(admin);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                // Delete the key and IV from storage
                KeyIvManager.DeleteKeyAndIv(admin.Email);
            }
            catch (Exception ex)
            {
                // Rollback the transaction if any exception occurs
                await transaction.RollbackAsync();

                // Handle specific database exceptions
                if (ex is SqlException)
                {
                    throw new AdminException("An error occurred while deleting the admin from the database.", ex);
                }

                throw; // Re-throw other exceptions
            }
        }


        public async Task RegisterAdminAsync(Admin admin)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
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

                // Call the DbContext method to execute the stored procedure
                await _context.RegisterAdminAsync(admin);

                // Commit the transaction
                await transaction.CommitAsync();

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
            catch (Exception ex)
            {
                // Rollback the transaction if any exception occurs
                await transaction.RollbackAsync();
                KeyIvManager.DeleteKeyAndIv(admin.Email);
                // Handle specific database update exceptions
                if (ex is SqlException)
                {
                    throw new AdminException("An error occurred while registering the admin.", ex);
                }

                throw; // Re-throw other exceptions
            }
        }

        public async Task UpdateAdminAsync(Admin admin)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            Admin existingAdmin = null;

            try
            {
                existingAdmin = await _context.Admins.FindAsync(admin.AdminID);
                if (existingAdmin == null)
                {
                    throw new AdminException("Admin not found");
                }

                // Check if an admin with the same email already exists
                if (await _context.Admins.AnyAsync(a => a.AdminID != admin.AdminID && a.Email == admin.Email))
                {
                    throw new AdminException("An admin with this email already exists.");
                }

                existingAdmin.Username = admin.Username;
                existingAdmin.FullName = admin.FullName;

                bool emailUpdated = existingAdmin.Email != admin.Email;
                bool passwordUpdated = existingAdmin.Password != admin.Password;

                if (emailUpdated)
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
                        Body = $"Dear {admin.FullName},\n\nYour admin account has been successfully verified.\n\nYour login credentials are:\nEmail: {admin.Email}\nPassword: {admin.Password}.\n\nBest regards,\nInsuranceApp Team"
                    };

                    string message = JsonSerializer.Serialize(emailDto);
                    _rabbitMqService.SendMessage(message);
                }

                if (passwordUpdated && !emailUpdated)
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

                await _context.UpdateAdminAsync(existingAdmin);
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // Rollback the transaction if any exception occurs
                await transaction.RollbackAsync();

                // Handle specific database update exceptions
                if (ex is SqlException)
                {
                    throw new AdminException("An error occurred while updating the admin in the database.", ex);
                }

                throw; // Re-throw other exceptions
            }
        }
    }
}
