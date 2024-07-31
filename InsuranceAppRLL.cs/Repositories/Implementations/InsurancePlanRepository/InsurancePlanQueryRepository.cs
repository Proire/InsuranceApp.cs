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
                var insurancePlans = await _context.GetAllInsurancePlansAsync();
                if (!insurancePlans.Any())
                {
                    throw new InsurancePlanException("Currently, we are not providing any insurance plans.");
                }
                return insurancePlans;
            }
            catch (SqlException ex)
            {
                throw new InsurancePlanException("An error occurred while retrieving insurance plans.", ex);
            }
            catch (Exception ex)
            {
                throw new InsurancePlanException("An unexpected error occurred while retrieving insurance plans.", ex);
            }
        }

        public async Task<InsurancePlan> GetInsurancePlan(int planId)
        {
            try
            {
                var insurancePlan = await _context.GetInsurancePlanByIdAsync(planId);
                if (insurancePlan == null)
                {
                    throw new InsurancePlanException($"No insurance plan found with ID {planId}.");
                }
                return insurancePlan;
            }
            catch (SqlException ex)
            {
                throw new InsurancePlanException("An error occurred while retrieving the insurance plan.", ex);
            }
            catch (Exception ex)
            {
                throw new InsurancePlanException("An unexpected error occurred while retrieving the insurance plan.", ex);
            }
        }
    }
}
