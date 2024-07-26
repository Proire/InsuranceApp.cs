using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Implementations.EmployeeRepository
{
    public class EmployeeQueryRepository : IEmployeeQueryRepository
    {
        private readonly InsuranceDbContext _context;

        public EmployeeQueryRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(employeeId);
                if (employee == null)
                {
                    throw new EmployeeException($"No Employee found with id: {employeeId}");
                }
                return employee;
            }
            catch (SqlException ex)
            {
                throw new EmployeeException("A database error occurred while retrieving the Employee.", ex);
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _context.Employees.ToListAsync();
                if (employees.Count == 0)
                {
                    throw new EmployeeException("No Employees found.");
                }
                return employees;
            }
            catch (SqlException ex)
            {
                throw new EmployeeException("A database error occurred while retrieving the Employees.", ex);
            }
        }
    }
}
