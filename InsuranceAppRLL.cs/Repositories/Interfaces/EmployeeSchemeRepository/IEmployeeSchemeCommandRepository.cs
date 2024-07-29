using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.EmployeeSchemeRepository
{
    public interface IEmployeeSchemeCommandRepository
    {
        public Task AddSchemeToEmployee(int schemeId, int employeeId);
        public Task DeleteSchemeFromEmployees(int schemeId);
        public Task DeleteEmployeeFromScheme (int employeeId);
    }
}
