using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.SchemeRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Implementations.SchemeRepository
{
    public class SchemeQueryRepository : ISchemeQueryRepository
    {
        private readonly InsuranceDbContext _context;

        public SchemeQueryRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Scheme>> GetAllSchemasForPlan(int planId)
        {
            try
            {
                List<Scheme> schemas = await _context.Schemes.Where(s => s.PlanID == planId).ToListAsync();
                if (schemas.Count() == 0)
                {
                    throw new SchemeException("Currently, we are not providing any Schema under this Plan");
                }
                return schemas;
            }
            catch(SchemeException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<Scheme> GetScheme(int schemeId)
        {
            try
            {
                Scheme? scheme = await _context.Schemes.FindAsync(schemeId);
                if (scheme == null)
                {
                    throw new SchemeException("Policy does not exists");
                }
                return scheme;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
