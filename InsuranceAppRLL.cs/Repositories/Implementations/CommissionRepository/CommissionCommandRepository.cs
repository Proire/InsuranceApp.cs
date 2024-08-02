using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.CommissionRepository;
using InsuranceMLL.CommissionModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
                var commissionAmountParam = new SqlParameter
                {
                    ParameterName = "@CommissionAmount",
                    SqlDbType = System.Data.SqlDbType.Float,
                    Direction = System.Data.ParameterDirection.Output
                };

                var parameters = new[]
                {
            new SqlParameter("@AgentID", commissionModel.AgentID),
            new SqlParameter("@PolicyID", commissionModel.PolicyID),
            new SqlParameter("@Amount", commissionModel.Amount),
            commissionAmountParam
        };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC AddCommission @AgentID, @PolicyID, @Amount, @CommissionAmount OUTPUT",
                    parameters);

                // You can now use the commissionAmountParam.Value if needed
                double commissionAmount = (double)commissionAmountParam.Value;
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions
                throw new Exception("An error occurred while adding the commission.", sqlEx);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An unexpected error occurred while adding the commission.", ex);
            }
        }

    }
}
