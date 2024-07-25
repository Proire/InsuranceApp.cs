using InsuranceAppRLL.CQRS.Queries.EmployeeQueries;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.EmployeeHandlers
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeQueryRepository _employeeQueryRepository;

        public GetEmployeeByIdQueryHandler(IEmployeeQueryRepository employeeQueryRepository)
        {
            _employeeQueryRepository = employeeQueryRepository;
        }

        public Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return _employeeQueryRepository.GetEmployeeByIdAsync(request.EmployeeId);
        }
    }
}
