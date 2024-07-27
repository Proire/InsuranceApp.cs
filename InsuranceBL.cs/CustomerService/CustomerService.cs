using InsuranceAppRLL.CQRS.Commands.CustomerCommands;
using InsuranceAppRLL.CQRS.Handlers.CustomerHandlers;
using InsuranceAppRLL.CQRS.Queries.CustomerQueries;
using InsuranceAppRLL.Entities;
using InsuranceMLL.CustomerModels;
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
            var command = new GetCustomerByAgentIdQuery(agentId);
            return _mediator.Send(command);
        }

        public async Task RegisterCustomerAsync(CustomerRegistrationModel customer, int agentId)
        {
            var command = new InsertCustomerCommand(customer.FullName, customer.Email, customer.Password, customer.Phone, customer.DateOfBirth,agentId);
            await _mediator.Send(command);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            await _mediator.Send(new DeleteCustomerCommand(customerId));
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _mediator.Send(new GetCustomerByIdQuery(customerId));
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _mediator.Send(new GetAllCustomersQuery());
        }

        public async Task UpdateCustomerAsync(CustomerUpdateModel customerUpdateModel, int customerId, int agentId)
        {
            await _mediator.Send(new UpdateCustomerCommand(
                customerId,
                customerUpdateModel.FullName,
                customerUpdateModel.Email,
                customerUpdateModel.Password,
                customerUpdateModel.Phone,
                customerUpdateModel.DateOfBirth,
                agentId
            ));
        }

    }
}
