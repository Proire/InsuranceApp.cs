using InsuranceAppRLL.CQRS.Queries.CustomerQueries;
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
    public class GetCustomersByAgentId : IRequestHandler<GetCustomerByIdQuery, IEnumerable<Customer>>
    {
        private readonly ICustomerQueryRepository _customerQueryRepository;
        public GetCustomersByAgentId(ICustomerQueryRepository customerQueryRepository)
        {
            _customerQueryRepository = customerQueryRepository;
        }
        public async Task<IEnumerable<Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _customerQueryRepository.GetCustomersByAgentId(request.AgentId);
        }
    }
}
