using InsuranceAppRLL.CQRS.Commands.EmployeeCommands;
using InsuranceAppRLL.CQRS.Queries.EmployeeQueries;
using InsuranceAppRLL.Entities;
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
            var command = new InsertEmployeeCommand(employee.Username, employee.Password, employee.Email, employee.FullName, employee.Role);
            await _mediator.Send(command);
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            await _mediator.Send(new DeleteEmployeeCommand(employeeId));
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _mediator.Send(new GetEmployeeByIdQuery(employeeId));
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _mediator.Send(new GetAllEmployeesQuery());
        }

        public async Task UpdateEmployeeAsync(EmployeeUpdateModel employee, int employeeId)
        {
            await _mediator.Send(new UpdateEmployeeCommand(employeeId,employee.Username, employee.Password, employee.Email, employee.FullName, employee.Role));
        }
    }
}
