using InsuranceAppRLL.CQRS.Commands.CustomerCommands;
using InsuranceAppRLL.Repositories.Interfaces.CustomerRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.CustomerHandlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerCommandRepository _customerCommandRepository;

        public DeleteCustomerCommandHandler(ICustomerCommandRepository customerCommandRepository)
        {
            _customerCommandRepository = customerCommandRepository;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerCommandRepository.DeleteCustomerAsync(request.CustomerId);
            return Unit.Value;
        }
    }
}
