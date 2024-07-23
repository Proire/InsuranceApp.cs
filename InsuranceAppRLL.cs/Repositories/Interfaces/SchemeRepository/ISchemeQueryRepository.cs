using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.SchemeRepository
{
    public interface ISchemeQueryRepository
    {
        public Task<List<Scheme>> getAllSchemasForPlan(int planId);
        public Task<Scheme> getScheme(int schemeId);
    }
}
