using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeSchemeRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Implementations.EmployeeSchemeRepository
{
    public class EmployeeSchemeCommandRepository : IEmployeeSchemeCommandRepository
    {
        private readonly InsuranceDbContext _context;

        public EmployeeSchemeCommandRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task AddSchemeToEmployee(int schemeId, int employeeId)
        {
            try
            {
                if(_context.EmployeeSchemes.Any(es=> es.EmployeeID==employeeId && es.SchemeID==schemeId))
                {
                    throw new EmployeeSchemeException("Employee with the specified Scheme already exists");
                }
                _context.EmployeeSchemes.Add(new EmployeeScheme {SchemeID=schemeId, EmployeeID=employeeId });
                await _context.SaveChangesAsync();
            }
            catch (EmployeeSchemeException ex)
            {
                throw new SchemeException(ex.Message);
            }
        }

        public async Task DeleteEmployeeFromScheme(int employeeId)
        {
            try
            {
                var employeeSchemes = await _context.EmployeeSchemes.Where(es => es.EmployeeID == employeeId).ToListAsync();
                _context.EmployeeSchemes.RemoveRange(employeeSchemes);
                await _context.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task DeleteSchemeFromEmployees(int schemeId)
        {
            try
            {
                var employeeSchemes = await _context.EmployeeSchemes.Where(es => es.SchemeID == schemeId).ToListAsync();
                _context.EmployeeSchemes.RemoveRange(employeeSchemes);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
