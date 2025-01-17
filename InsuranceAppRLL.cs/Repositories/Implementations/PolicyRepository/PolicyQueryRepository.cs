﻿using InsuranceAppRLL.CustomExceptions;
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
    public class PolicyQueryRepository : IPolicyQueryRepository
    {
        private readonly InsuranceDbContext _context;

        public PolicyQueryRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Policy>> GetAllPoliciesForCustomer(int customerId)
        {
            try
            {
                List<Policy> policies = await _context.Policies.Where(p=>p.CustomerID==customerId).ToListAsync();
                if (policies == null)
                {
                    throw new PolicyException("Customer have not purchased any insurance Plan");
                }
                return policies;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<Policy> GetPolicy(int policyId)
        {
            try
            {
                Policy? policy = await _context.Policies.FindAsync(policyId);
                if (policy == null)
                {
                    throw new PolicyException("Policy does not exists");
                }
                return policy;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
