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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int planId = await _context.AddInsurancePlanAsync(plan.PlanName, plan.PlanDetails);
                plan.PlanID = planId;
                await transaction.CommitAsync();
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                throw new InsurancePlanException("An error occurred while adding the plan.", ex);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InsurancePlanException("An unexpected error occurred.", ex);
            }
        }

        public async Task DeletePlan(int planId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.DeleteInsurancePlanAsync(planId);
                await transaction.CommitAsync();
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                throw new InsurancePlanException("An error occurred while deleting the plan.", ex);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InsurancePlanException("An unexpected error occurred.", ex);
            }
        }

        public async Task UpdatePlan(InsurancePlan plan)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.UpdateInsurancePlanAsync(plan.PlanID, plan.PlanName, plan.PlanDetails);
                await transaction.CommitAsync();
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                throw new InsurancePlanException("An error occurred while updating the plan.", ex);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InsurancePlanException("An unexpected error occurred.", ex);
            }
        }
    }
}
