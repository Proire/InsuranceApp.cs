using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Repositories.Interfaces.CommissionRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Implementations.CommissionRepository
{
    public class CommissionQueryRepository:ICommissionQueryRepository
    {
        private readonly InsuranceDbContext _context;

        public CommissionQueryRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<double> GetTotalCommissionForAgentAsync(int agentId)
        {
            try
            {
                // Use the DbContext method to call the stored procedure
                var totalCommission = await _context.GetTotalCommissionForAgentAsync(agentId);

                if (totalCommission <= 0)
                {
                    throw new CommissionException("Agent does not have any commission yet.");
                }

                return totalCommission;
            }
            catch (SqlException ex)
            {
                throw new CommissionException("An error occurred while retrieving the total commission for the agent.", ex);
            }
        }
    }
}
