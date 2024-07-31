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
                var parameter = new SqlParameter("@CustomerID", customerId);
                var payments = await _context.Payments.FromSqlRaw("EXEC GetPaymentsForCustomer @CustomerID", parameter).ToListAsync();

                if (!payments.Any())
                {
                    throw new PaymentException("Customer does not have any payments.");
                }

                return payments;
            }
            catch (SqlException ex)
            {
                throw new PaymentException("An error occurred while retrieving payments for the customer.", ex);
            }
            catch (Exception ex)
            {
                throw new PaymentException("An unexpected error occurred while retrieving payments for the customer.", ex);
            }
        }

        public async Task<List<Payment>> GetAllPaymentsForPolicyAsync(int policyId)
        {
            try
            {
                var parameter = new SqlParameter("@PolicyID", policyId);
                var payments = await _context.Payments.FromSqlRaw("EXEC GetPaymentsForPolicy @PolicyID", parameter).ToListAsync();

                if (!payments.Any())
                {
                    throw new PaymentException("Policy does not have any payments.");
                }

                return payments;
            }
            catch (SqlException ex)
            {
                throw new PaymentException("An error occurred while retrieving payments for the policy.", ex);
            }
            catch (Exception ex)
            {
                throw new PaymentException("An unexpected error occurred while retrieving payments for the policy.", ex);
            }
        }
    }
}
