using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsuranceAgentRepository;
using InsuranceAppRLL.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserModelLayer;

namespace InsuranceAppRLL.Repositories.Implementations.InsuranceAgentRepository
{
    public class InsuranceAgentCommandRepository : IInsuranceAgentCommandRepository
    {
        private readonly InsuranceDbContext _context;
        private readonly RabitMQProducer _rabbitMqService;

        public InsuranceAgentCommandRepository(InsuranceDbContext context, RabitMQProducer rabbitMqService)
        {
            _context = context;
            _rabbitMqService = rabbitMqService;
        }

        public async Task RegisterInsuranceAgentAsync(InsuranceAgent insuranceAgent)
        {
            string password = insuranceAgent.Password;
            // Generate a unique key and IV for the insurance agent
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                byte[] key = aes.Key;
                byte[] iv = aes.IV;

                // Store the key and IV in a file or secure storage
                KeyIvManager.SaveKeyAndIv(insuranceAgent.Email, key, iv);

                // Hash the insurance agent's password using the generated key and IV
                insuranceAgent.Password = PasswordHasher.HashPassword(insuranceAgent.Password, key, iv);
            }

            try
            {
                await _context.InsuranceAgents.AddAsync(insuranceAgent);
                await _context.SaveChangesAsync();

                // Send confirmation email with credentials using RabbitMQ
                var emailDto = new EmailDTO
                {
                    To = insuranceAgent.Email,
                    Subject = "Insurance Agent Registration Confirmation",
                    Body = $"Dear {insuranceAgent.FullName},\n\nYour insurance agent account has been successfully created.\n\nYour login credentials are:\nEmail: {insuranceAgent.Email}\nPassword: {password}.\n\nBest regards,\nInsuranceApp Team"
                };

                string message = JsonSerializer.Serialize(emailDto);
                _rabbitMqService.SendMessage(message);
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new InsuranceAgentException("An error occurred while registering the insurance agent.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new InsuranceAgentException("An unexpected error occurred.", ex);
            }
        }
    }
}
