using InsuranceAppRLL.CQRS.Commands.CustomerCommands;
using InsuranceAppRLL.CQRS.Handlers.CustomerHandlers;
using InsuranceAppRLL.CQRS.Queries.CustomerQueries;
using InsuranceAppRLL.Entities;
using InsuranceMLL.CustomerModels;
using InsuranceMLL.CustomerModels.InsuranceMLL.CustomerModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator _mediator;

        public CustomerService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<IEnumerable<Customer>> GetCustomers(int agentId)
        {
            var command = new GetCustomerByIdQuery(agentId);
            return _mediator.Send(command);
        }

        public async Task RegisterCustomerAsync(CustomerRegistrationModel customer)
        {
            var command = new InsertCustomerCommand(customer.FullName, customer.Email, customer.Password, customer.Phone, customer.DateOfBirth,customer.AgentId);
            await _mediator.Send(command);
        }
    }
}
