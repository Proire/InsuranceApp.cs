using InsuranceAppRLL.Entities;
using InsuranceMLL.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.EmployeeService
{
    public interface IEmployeeService
    {
        Task RegisterEmployeeAsync(EmployeeRegistrationModel employee);

        Task DeleteEmployeeAsync(int employeeId);

        Task UpdateEmployeeAsync(EmployeeUpdateModel employee, int employeeId);

        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int employeeId);
    }
}
