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

        public async Task<IList<InsurancePlan>> getAllInsurancePlans()
        {
            try
            {
                List<InsurancePlan> insurancePlans = await _context.InsurancePlans.ToListAsync();
                if(insurancePlans==null)
                {
                    throw new InsurancePlanException("Currently, we are not Providing any Insurance Plan");
                }
                return insurancePlans;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<InsurancePlan> getInsurancePlan(int planId)
        {
            try
            {
                InsurancePlan? insurancePlan = await _context.InsurancePlans.FindAsync(planId);
                if (insurancePlan == null)
                {
                    throw new InsurancePlanException("Insurance Plan does not exists");
                }
                return insurancePlan;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
