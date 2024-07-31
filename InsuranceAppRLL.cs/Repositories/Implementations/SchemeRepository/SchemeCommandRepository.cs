using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeSchemeRepository;
using InsuranceAppRLL.Repositories.Interfaces.SchemeRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InsuranceAppRLL.Repositories.Implementations.SchemeRepository
{
    public class SchemeCommandRepository : ISchemeCommandRepository
    {
        private readonly InsuranceDbContext _context;
        private readonly IEmployeeSchemeCommandRepository _employeeSchemeCommand;

        public SchemeCommandRepository(InsuranceDbContext context, IEmployeeSchemeCommandRepository employeeSchemeCommand)
        {
            _context = context;
            _employeeSchemeCommand = employeeSchemeCommand;
        }

        public async Task AddScheme(Scheme scheme, int employeeId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var duplicateSchemeName = new SqlParameter("@SchemeName", scheme.SchemeName);
                    var schemeExists = await _context.Database.ExecuteSqlRawAsync(
                        "EXEC CheckDuplicateSchemeName @SchemeName", duplicateSchemeName);

                    if (schemeExists > 0)
                    {
                        throw new SchemeException("Scheme with the specified name already exists.");
                    }

                    var planExists = new SqlParameter("@PlanID", scheme.PlanID);
                    var planCheck = await _context.Database.ExecuteSqlRawAsync(
                        "EXEC CheckInsurancePlanExists @PlanID", planExists);

                    if (planCheck == 0)
                    {
                        throw new InsurancePlanException($"Plan with specified planId {scheme.PlanID} does not exist.");
                    }

                    var addSchemeParams = new[]
                    {
                        new SqlParameter("@SchemeName", scheme.SchemeName),
                        new SqlParameter("@SchemeDetails", scheme.SchemeDetails),
                        new SqlParameter("@SchemePrice", scheme.SchemePrice),
                        new SqlParameter("@SchemeCover", scheme.SchemeCover),
                        new SqlParameter("@SchemeTenure", scheme.SchemeTenure),
                        new SqlParameter("@PlanID", scheme.PlanID)
                    };

                    var schemeIdParam = new SqlParameter
                    {
                        ParameterName = "@SchemeID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC AddScheme @SchemeName, @SchemeDetails, @SchemePrice, @SchemeCover, @SchemeTenure, @PlanID, @SchemeID OUTPUT",
                        addSchemeParams.Concat(new[] { schemeIdParam }).ToArray());

                    scheme.SchemeID = (int)schemeIdParam.Value;

                    await _employeeSchemeCommand.AddSchemeToEmployee(scheme.SchemeID, employeeId);
                    await transaction.CommitAsync();
                }
                catch (SqlException ex)
                {
                    await transaction.RollbackAsync();
                    throw new SchemeException("An error occurred while adding the scheme.", ex);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new SchemeException("An unexpected error occurred while adding the scheme.", ex);
                }
            }
        }

        public async Task UpdateScheme(Scheme scheme)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingScheme = new SqlParameter("@SchemeID", scheme.SchemeID);
                    var existingSchemeData = await _context.Database.ExecuteSqlRawAsync(
                        "EXEC GetSchemeByID @SchemeID", existingScheme);

                    if (existingSchemeData == null)
                    {
                        throw new SchemeException("Scheme with the specified ID does not exist.");
                    }

                    var duplicateSchemeNameParams = new[]
                    {
                        new SqlParameter("@SchemeName", scheme.SchemeName),
                        new SqlParameter("@SchemeID", scheme.SchemeID)
                    };

                    var nameExists = await _context.Database.ExecuteSqlRawAsync(
                        "EXEC CheckDuplicateSchemeNameForUpdate @SchemeName, @SchemeID", duplicateSchemeNameParams);

                    if (nameExists > 0)
                    {
                        throw new SchemeException("Another scheme with the specified name already exists.");
                    }

                    var updateSchemeParams = new[]
                    {
                        new SqlParameter("@SchemeID", scheme.SchemeID),
                        new SqlParameter("@SchemeName", scheme.SchemeName),
                        new SqlParameter("@SchemeDetails", scheme.SchemeDetails),
                        new SqlParameter("@PlanID", scheme.PlanID)
                    };

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC UpdateScheme @SchemeID, @SchemeName, @SchemeDetails, @PlanID",
                        updateSchemeParams);

                    await transaction.CommitAsync();
                }
                catch (SqlException ex)
                {
                    await transaction.RollbackAsync();
                    throw new SchemeException("An error occurred while updating the scheme.", ex);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new SchemeException("An unexpected error occurred while updating the scheme.", ex);
                }
            }
        }

        public async Task DeleteScheme(int schemeId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var schemeExists = new SqlParameter("@SchemeID", schemeId);
                    var schemeCheck = await _context.Database.ExecuteSqlRawAsync(
                        "EXEC CheckSchemeExists @SchemeID", schemeExists);

                    if (schemeCheck == 0)
                    {
                        throw new SchemeException("Scheme with the specified ID does not exist or has already been deleted.");
                    }

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC DeleteScheme @SchemeID", schemeExists);

                    await _employeeSchemeCommand.DeleteSchemeFromEmployees(schemeId);
                    await transaction.CommitAsync();
                }
                catch (SqlException ex)
                {
                    await transaction.RollbackAsync();
                    throw new SchemeException("An error occurred while deleting the scheme.", ex);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new SchemeException("An unexpected error occurred while deleting the scheme.", ex);
                }
            }
        }
    }
}
