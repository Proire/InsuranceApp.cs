using BookStoreRL.Utilities;
using InsuranceAppRLL.CQRS.Commands.EmployeeCommands;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeRepository;
using InsuranceAppRLL.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserModelLayer;

namespace InsuranceAppRLL.CQRS.Handlers.EmployeeHandlers
{
    public class InsertEmployeeCommandHandler : IRequestHandler<InsertEmployeeCommand, Employee>
    {
        private readonly IEmployeeCommandRepository _repository;

        public InsertEmployeeCommandHandler(IEmployeeCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<Employee> Handle(InsertEmployeeCommand request, CancellationToken cancellationToken)
        {


            var employee = new Employee
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                FullName = request.FullName,
                Role = request.Role,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.RegisterEmployeeAsync(employee);
            return employee;
        }
    }
}
