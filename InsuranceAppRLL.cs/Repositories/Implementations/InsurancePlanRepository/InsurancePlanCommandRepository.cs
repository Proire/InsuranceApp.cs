using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsurancePlanRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace InsuranceAppRLL.Repositories.Implementations.InsurancePlanRepository
{
    public class InsurancePlanCommandRepository : IInsurancePlanCommandRepository
    {
        private readonly InsuranceDbContext _context;

        public InsurancePlanCommandRepository(InsuranceDbContext context)
        {
            _context = context;
        }
        public async Task AddPlan(InsurancePlan plan)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.InsurancePlans.AddAsync(plan);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch(SqlException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
