using InsuranceAppRLL.CQRS.Queries.AdminQueries;
using InsuranceAppRLL.CQRS.Queries.EmployeeQueries;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.EmployeeHandlers
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IEmployeeQueryRepository _employeeQueryRepository;

        public GetAllEmployeesQueryHandler(IEmployeeQueryRepository employeeQueryRepository)
        {
            _employeeQueryRepository = employeeQueryRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _employeeQueryRepository.GetAllEmployeesAsync();
        }
    }
}
