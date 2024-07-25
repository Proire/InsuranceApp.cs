using InsuranceAppRLL.CQRS.Commands.EmployeeCommands;
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
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeCommandRepository _employeeCommandRepository;

        public UpdateEmployeeCommandHandler(IEmployeeCommandRepository employeeCommandRepository)
        {
            _employeeCommandRepository = employeeCommandRepository;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                EmployeeID = request.EmployeeId,
                Username = request.Username,
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password
            };

            await _employeeCommandRepository.UpdateEmployeeAsync(employee);
            return Unit.Value;
        }
    }
}
