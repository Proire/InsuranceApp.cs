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
                var commission = await _context.Commissions.Where(c=>c.AgentID==agentId).FirstAsync();
                if(commission==null)
                {
                    throw new CommissionException("Agent does not have any commission yet");
                }
                return commission.CommissionAmount;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
