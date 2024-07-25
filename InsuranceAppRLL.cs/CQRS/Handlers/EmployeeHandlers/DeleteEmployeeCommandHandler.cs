using InsuranceAppRLL.CQRS.Commands.EmployeeCommands;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.EmployeeHandlers
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeCommandRepository _employeeCommandRepository;

        public DeleteEmployeeCommandHandler(IEmployeeCommandRepository employeeCommandRepository)
        {
            _employeeCommandRepository = employeeCommandRepository;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeCommandRepository.DeleteEmployeeAsync(request.EmployeeId);
            return Unit.Value;
        }
    }
}
