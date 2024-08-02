using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeSchemeRepository;
using Microsoft.Data.SqlClient;
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
                await _context.AddSchemeToEmployeeAsync(schemeId, employeeId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw new EmployeeSchemeException("An error occurred while adding the scheme to the employee.", ex);
            }
            catch (Exception ex)
            {
                throw new EmployeeSchemeException("An unexpected error occurred.", ex);
            }
        }

        public async Task DeleteEmployeeFromScheme(int employeeId)
        {
            try
            {
                await _context.DeleteEmployeeFromSchemeAsync(employeeId);
            }
            catch (SqlException ex)
            {
              
                throw new EmployeeSchemeException("An error occurred while deleting the employee from schemes.", ex);
            }
            catch (Exception ex)
            {
               
                throw new EmployeeSchemeException("An unexpected error occurred.", ex);
            }
        }

        public async Task DeleteSchemeFromEmployees(int schemeId)
        {
            
            try
            {
                await _context.DeleteSchemeFromEmployeesAsync(schemeId);
             
            }
            catch (SqlException ex)
            {
              Console.WriteLine(ex.Message);
                throw new EmployeeSchemeException("An error occurred while deleting the scheme from employees.", ex);
            }
            catch (Exception ex)
            {
               
                throw new EmployeeSchemeException("An unexpected error occurred.", ex);
            }
        }
    }
}
