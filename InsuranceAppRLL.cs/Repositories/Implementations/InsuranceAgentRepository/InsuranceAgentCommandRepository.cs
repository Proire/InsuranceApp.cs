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
            try
            {
                if (await _context.InsuranceAgents.AnyAsync(a => a.Email == insuranceAgent.Email))
                {
                    throw new InsuranceAgentException("An agent with this email already exists.");
                }


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
            catch(InsuranceAgentException)
            {
                throw;
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

        public async Task DeleteAgentAsync(int agentId)
        {
            try
            {
                var agent = await _context.InsuranceAgents.FindAsync(agentId);
                if (agent != null)
                {
                    _context.InsuranceAgents.Remove(agent);
                    await _context.SaveChangesAsync();
                    KeyIvManager.DeleteKeyAndIv(agent.Email);
                }
                else
                {
                    throw new InsuranceAgentException($"No agent found with id: {agentId}");
                }
            }
            catch (InsuranceAgentException)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new InsuranceAgentException("An error occurred while deleting the agent from the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new InsuranceAgentException("An unexpected error occurred.", ex);
            }
        }

        public async Task UpdateAgentAsync(InsuranceAgent agent)
        {
            try
            {
               

                var existingAgent = await _context.InsuranceAgents.FindAsync(agent.AgentID);
                if (existingAgent == null)
                {
                    throw new InsuranceAgentException($"No agent found with id: {agent.AgentID}");
                }
                if (await _context.InsuranceAgents.AnyAsync(a => a.AgentID != agent.AgentID && a.Email == agent.Email))
                {
                    throw new InsuranceAgentException("An agent with this email already exists.");
                }


                existingAgent.Username = agent.Username;
                existingAgent.FullName = agent.FullName;

                if (existingAgent.Email != agent.Email)
                {
                    // Generate a unique key and IV for the agent
                    using (var aes = System.Security.Cryptography.Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;

                        // Store the key and IV in a file or secure storage
                        KeyIvManager.SaveKeyAndIv(agent.Email, key, iv);

                        // Hash the agent's password using the generated key and IV
                        existingAgent.Password = PasswordHasher.HashPassword(agent.Password, key, iv);
                        existingAgent.Email = agent.Email;
                    }

                    // Send confirmation email with credentials using RabbitMQ
                    var emailDto = new EmailDTO
                    {
                        To = agent.Email,
                        Subject = "Insurance Agent Registration Confirmation",
                        Body = $"Dear {agent.FullName},\n\nYour insurance agent account has been successfully created.\n\nYour login credentials are:\nEmail: {agent.Email}\nPassword: {agent.Password}.\n\nBest regards,\nInsuranceApp Team"
                    };

                    string message = JsonSerializer.Serialize(emailDto);
                    _rabbitMqService.SendMessage(message);
                }
                else if (existingAgent.Password != agent.Password)
                {
                    // Generate a unique key and IV for the agent
                    using (var aes = System.Security.Cryptography.Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;

                        // Store the key and IV in a file
                        KeyIvManager.UpdateKeyAndIv(agent.Email, key, iv);

                        // Hash the agent's password using the generated key and IV
                        existingAgent.Password = PasswordHasher.HashPassword(agent.Password, key, iv);
                    }
                }

                _context.InsuranceAgents.Update(existingAgent);
                await _context.SaveChangesAsync();
            }
            catch (InsuranceAgentException)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflicts
                throw new InsuranceAgentException("A concurrency conflict occurred while updating the agent.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new InsuranceAgentException("An error occurred while updating the agent in the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new InsuranceAgentException("An unexpected error occurred.", ex);
            }
        }
    }
}
