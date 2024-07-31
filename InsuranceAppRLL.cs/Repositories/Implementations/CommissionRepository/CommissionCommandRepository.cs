using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.CommissionRepository;
using InsuranceMLL.CommissionModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Implementations.CommissionRepository
{
    public class CommissionCommandRepository : ICommissionCommandRepository
    {
        private readonly InsuranceDbContext _context;

        public CommissionCommandRepository(InsuranceDbContext context)
        {
            _context = context;
        }
        public async Task AddCommissionAsync(CommissionModel commissionModel)
        {
            try
            {
                var commissionCheck = await _context.Commissions.FindAsync(commissionModel.PolicyID);

                if (commissionModel.AgentID != 0) {
                    Commission commission = new Commission();
                    commission.PolicyID = commissionModel.PolicyID;
                    commissionModel.AgentID = commissionModel.AgentID;

                    if (commissionCheck == null)
                    {
                        commission.CommissionAmount = 0.2 * commissionModel.Amount;
                    }
                    else if (commissionCheck != null)
                    {
                        commission.CommissionAmount = 0.125 * commissionModel.Amount;
                    }
                    await _context.Commissions.AddAsync(commission);
                    await _context.SaveChangesAsync();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
        }
    }
}
