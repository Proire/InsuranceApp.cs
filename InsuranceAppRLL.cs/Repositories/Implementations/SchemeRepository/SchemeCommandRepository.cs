using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.SchemeRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InsuranceAppRLL.Repositories.Implementations.SchemeRepository
{
    public class SchemeCommandRepository : ISchemeCommandRepository
    {
        private readonly InsuranceDbContext _context;

        public SchemeCommandRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task AddScheme(Scheme scheme)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Check for duplicate scheme name
                    if (_context.Schemes.Any(s => s.SchemeName.Equals(scheme.SchemeName)))
                    {
                        throw new SchemeException("Scheme with the specified name already exists.");
                    }

                    await _context.Schemes.AddAsync(scheme);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (SqlException ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task UpdateScheme(Scheme scheme)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingScheme = await _context.Schemes.FindAsync(scheme.SchemeID);
                    if (existingScheme == null)
                    {
                        throw new SchemeException("Scheme with the specified ID does not exist.");
                    }

                    // Check for duplicate scheme name, excluding the current scheme
                    if (_context.Schemes.Any(s => s.SchemeName.Equals(scheme.SchemeName) && s.SchemeID != scheme.SchemeID))
                    {
                        throw new SchemeException("Another scheme with the specified name already exists.");
                    }

                    existingScheme.SchemeName = scheme.SchemeName;
                    existingScheme.SchemeDetails = scheme.SchemeDetails;
                    existingScheme.PlanID = scheme.PlanID; // Update PlanID if needed
                    // Update other properties if necessary

                    _context.Schemes.Update(existingScheme);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (SqlException ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task DeleteScheme(int schemeId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var scheme = await _context.Schemes.FindAsync(schemeId);
                    if (scheme == null)
                    {
                        throw new SchemeException("Scheme with the specified ID does not exist or has already been deleted.");
                    }

                    _context.Schemes.Remove(scheme);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (SqlException ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
