using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries.EmployeeQueries
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<Employee>>
    {
    }
}
