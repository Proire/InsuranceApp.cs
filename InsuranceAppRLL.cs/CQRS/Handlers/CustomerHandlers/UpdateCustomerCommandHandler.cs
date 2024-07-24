using InsuranceAppRLL.CQRS.Commands.CustomerCommands;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.CustomerRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.CustomerHandlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerCommandRepository _customerCommandRepository;

        public UpdateCustomerCommandHandler(ICustomerCommandRepository customerCommandRepository)
        {
            _customerCommandRepository = customerCommandRepository;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer customer = new Customer()
            {
                CustomerID = request.CustomerId,
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password,
                Phone = request.Phone,
                DateOfBirth = request.DateOfBirth,
                AgentID = request.AgentId
            };

            await _customerCommandRepository.UpdateCustomerAsync(customer);
            return Unit.Value;
        }
    }
}
