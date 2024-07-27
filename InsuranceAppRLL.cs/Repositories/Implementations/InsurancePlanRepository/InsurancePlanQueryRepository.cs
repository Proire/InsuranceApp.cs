using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsurancePlanRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRLL.Utilities;

namespace InsuranceAppRLL.Repositories.Implementations.InsurancePlanRepository
{
    public class InsurancePlanQueryRepository : IInsurancePlanQueryRepository
    {
        private readonly InsuranceDbContext _context;

        public InsurancePlanQueryRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<List<InsurancePlan>> GetAllInsurancePlans()
        {
            try
            {
                List<InsurancePlan> insurancePlans = await _context.InsurancePlans.ToListAsync();
                if(insurancePlans.Count() == 0)
                {
                    throw new InsurancePlanException("Currently, we are not Providing any Insurance Plan");
                }
                return insurancePlans;
            }
            catch (InsurancePlanException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<InsurancePlan> GetInsurancePlan(int planId)
        {
            try
            {
                InsurancePlan? insurancePlan = await _context.InsurancePlans.FindAsync(planId);
                if (insurancePlan == null)
                {
                    throw new InsurancePlanException($"No Insurance Plan with planId {planId} exists");
                }
                return insurancePlan;
            }
            catch(InsurancePlanException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
