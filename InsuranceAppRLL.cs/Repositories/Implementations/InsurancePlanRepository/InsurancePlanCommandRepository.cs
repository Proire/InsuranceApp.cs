using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsurancePlanRepository;
using Microsoft.Data.SqlClient;

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
                    if(_context.InsurancePlans.Any(p=>p.PlanName.Equals(plan.PlanName)))
                    {
                        throw new InsurancePlanException("Plan with the Specified Name already exists");
                    }
                    await _context.InsurancePlans.AddAsync(plan);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch(SqlException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task DeletePlan(int planId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var plan = await _context.InsurancePlans.FindAsync(planId);
                    if (plan == null)
                    {
                        throw new InsurancePlanException("Plan with the specified Id already deleted or does not Exists");
                    }
                    _context.InsurancePlans.Remove(plan);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task UpdatePlan(InsurancePlan plan)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var plancheck = await _context.InsurancePlans.FindAsync(plan.PlanID);
                    if (plancheck == null)
                    {
                        throw new InsurancePlanException("Plan with the specified Id does not Exists");
                    }
                    if (plancheck.PlanName.Equals(plan.PlanName))
                    {
                        throw new InsurancePlanException("Plan with the specified Name already Exists");
                    }
                    plancheck.PlanName = plan.PlanName;
                    plancheck.PlanDetails = plan.PlanDetails;   
                    _context.InsurancePlans.Update(plancheck);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
