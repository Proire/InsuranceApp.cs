using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsuranceAgentRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var agent = await _context.InsuranceAgents.FindAsync(agentId);
                if (agent == null)
                {
                    throw new InsuranceAgentException($"No insurance agent found with id: {agentId}");
                }
                return agent;
            }
            catch (Exception ex)
            {
                throw new InsuranceAgentException("An error occurred while retrieving the insurance agent.", ex);
            }
        }


        public async Task<IEnumerable<InsuranceAgent>> GetAllAsync()
        {
            try
            {
                var agents = await _context.InsuranceAgents.ToListAsync();
                if (agents.Count == 0)
                {
                    throw new InsuranceAgentException("No insurance agents found.");
                }
                return agents;
            }
            catch (Exception ex)
            {
                throw new InsuranceAgentException("An error occurred while retrieving the insurance agents.", ex);
            }
        }
    }
}
