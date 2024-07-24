﻿using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.PolicyRepository
{
    public interface IPolicyQueryRepository
    {
        public Task<List<Policy>> getAllPoliciesForCustomer(int customerId);
        public Task<Policy> getPolicy(int policyId);
    }
}