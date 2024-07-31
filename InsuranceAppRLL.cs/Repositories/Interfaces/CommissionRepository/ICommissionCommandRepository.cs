using InsuranceAppRLL.Entities;
using InsuranceMLL.CommissionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.CommissionRepository
{
    public interface ICommissionCommandRepository
    {
        public Task AddCommissionAsync(CommissionModel commissionModel);
    }
}
