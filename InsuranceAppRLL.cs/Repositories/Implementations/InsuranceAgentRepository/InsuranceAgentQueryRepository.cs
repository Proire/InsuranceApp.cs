using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsuranceAgentRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAppRLL.Repositories.Implementations.InsuranceAgentRepository
{
    public class InsuranceAgentQueryRepository : IInsuranceAgentQueryRepository
    {
        private readonly InsuranceDbContext _context;

        public InsuranceAgentQueryRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<InsuranceAgent> GetAgentByIDAsync(int agentId)
        {
            try
            {
                var agent = await _context.GetAgentByIDAsync(agentId);
                if (agent == null)
                {
                    throw new InsuranceAgentException($"No insurance agent found with id: {agentId}");
                }
                return agent;
            }
            catch (SqlException ex)
            {
                throw new InsuranceAgentException("An error occurred while retrieving the insurance agent.", ex);
            }
        }

        public async Task<IEnumerable<InsuranceAgent>> GetAllAsync()
        {
            try
            {
                var agents = await _context.GetAllAsync();
                if (!agents.Any())
                {
                    throw new InsuranceAgentException("No insurance agents found.");
                }
                return agents;
            }
            catch (SqlException ex)
            {
                throw new InsuranceAgentException("An error occurred while retrieving the insurance agents.", ex);
            }
        }
    }
}
