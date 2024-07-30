using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.PolicyRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Implementations.PolicyRepository
{
    public class PolicyCommandRepository : IPolicyCommandRepository
    {
        private readonly InsuranceDbContext _context;

        public PolicyCommandRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task AddPolicy(Policy policy)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var customerIDParam = new SqlParameter("@CustomerID", policy.CustomerID);
                    var schemeIDParam = new SqlParameter("@SchemeID", policy.SchemeID);

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC CheckPolicyExists @CustomerID, @SchemeID",
                        customerIDParam, schemeIDParam);

                    await _context.Policies.AddAsync(policy);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (SqlException)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task UpdatePolicy(Policy policy)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingPolicy = await _context.Policies.FindAsync(policy.PolicyID);
                    if (existingPolicy == null)
                    {
                        throw new PolicyException("Policy with the specified ID does not exist");
                    }

                    // While updating, does any policy with new policy details for the same customer already exist? If yes, then not allowed to update
                    if (_context.Policies.Any(p => p.PolicyDetails.Equals(policy.PolicyDetails) && p.CustomerID == policy.CustomerID && p.PolicyID != policy.PolicyID))
                    {
                        throw new PolicyException("Another policy with the specified details already exists for this customer");
                    }

                    existingPolicy.PolicyDetails = policy.PolicyDetails;
                    existingPolicy.Premium = policy.Premium;
                    existingPolicy.DateIssued = policy.DateIssued;
                    existingPolicy.MaturityPeriod = policy.MaturityPeriod;
                    existingPolicy.PolicyLapseDate = policy.PolicyLapseDate;

                    _context.Policies.Update(existingPolicy);
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

        public async Task DeletePolicy(int policyId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var policy = await _context.Policies.FindAsync(policyId);
                    if (policy == null)
                    {
                        throw new PolicyException("Policy with the specified ID already deleted or does not exist");
                    }
                    _context.Policies.Remove(policy);
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
