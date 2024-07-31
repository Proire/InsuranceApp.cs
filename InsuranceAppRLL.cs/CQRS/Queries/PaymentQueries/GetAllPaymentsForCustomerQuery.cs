using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries.PaymentQueries
{
    public class GetAllPaymentsForCustomerQuery : IRequest<List<Payment>>
    {
        public int CustomerId { get; set; }

        public GetAllPaymentsForCustomerQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
