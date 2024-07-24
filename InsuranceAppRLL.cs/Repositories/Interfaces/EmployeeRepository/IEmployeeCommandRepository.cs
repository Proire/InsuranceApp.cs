using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.EmployeeRepository
{
    public interface IEmployeeCommandRepository
    {
        Task RegisterEmployeeAsync(Employee employee);
    }
}
