﻿using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.InsuranceAgentRepository
{
    public interface IInsuranceAgentCommandRepository
    {
        Task RegisterInsuranceAgentAsync(InsuranceAgent insuranceAgent);

        Task UpdateAgentAsync(InsuranceAgent agent);

        Task DeleteAgentAsync(int agentId);
    }
}
