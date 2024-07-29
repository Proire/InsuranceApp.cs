using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.PaymentRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Implementations.PaymentRepository
{
    public class PaymentQueryRepository : IPaymentQueryRepository
    {
        private readonly InsuranceDbContext _context;

        public PaymentQueryRepository(InsuranceDbContext context)
        {
            _context = context;
        }
        public async Task<List<Payment>> GetAllPaymentsForCustomerAsync(int customerId)
        {
            try
            {
                var payments =  await _context.Payments.Where(p=>p.CustomerID== customerId).ToListAsync();
                if(!payments.Any() )
                {
                    throw new PaymentException("Customer does not have any payments");
                }
                return payments;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<List<Payment>> GetAllPaymentsForPolicyAsync(int policyId)
        {
            try
            {
                var payments = await _context.Payments.Where(p => p.PolicyID == policyId).ToListAsync();
                if (!payments.Any())
                {
                    throw new PaymentException("Policy do not have any payments");
                }
                return payments;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
