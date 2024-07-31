using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.PaymentRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Implementations.PaymentRepository
{
    public class PaymentCommandRepository : IPaymentCommandRepository
    {
        private readonly InsuranceDbContext _context;

        public PaymentCommandRepository(InsuranceDbContext context)
        {
            _context = context;
        }
        public async Task AddPaymentAsync(Payment payment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Add payment using stored procedure
                    await _context.AddPaymentAsync(payment.CustomerID, payment.PolicyID, payment.Amount);

                    // Commit transaction
                    await transaction.CommitAsync();
                }
                catch (SqlException ex)
                {
                    await transaction.RollbackAsync();
                    throw new PaymentException("An error occurred while adding the payment.", ex);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new PaymentException("An unexpected error occurred while adding the payment.", ex);
                }
            }
        }
    }
}
