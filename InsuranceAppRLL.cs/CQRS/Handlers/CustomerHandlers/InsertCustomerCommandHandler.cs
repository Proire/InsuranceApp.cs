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
    public class InsertCustomerCommandHandler : IRequestHandler<InsertCustomerCommand, Customer>
    {
        private readonly ICustomerCommandRepository _repository;

        public InsertCustomerCommandHandler(ICustomerCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Handle(InsertCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password,
                Phone = request.Phone,
                DateOfBirth = request.DateOfBirth,
                AgentID = request.AgentID,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.RegisterCustomerAsync(customer);
            return customer;
        }
    }
}
