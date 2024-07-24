using InsuranceAppRLL.CQRS.Commands.EmployeeCommands;
using InsuranceMLL.EmployeeModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMediator _mediator;

        public EmployeeService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task RegisterEmployeeAsync(EmployeeRegistrationModel employee)
        {
            var command = new InsertEmployeeCommand(employee.Username, employee.Password, employee.Email, employee.FullName);
            await _mediator.Send(command);
        }
    }
}
