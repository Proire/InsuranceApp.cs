using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.CommissionRepository;
using InsuranceAppRLL.Repositories.Interfaces.PaymentRepository;
using InsuranceMLL.CommissionModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICommissionCommandRepository commissionCommandRepository;

        public PaymentCommandRepository(InsuranceDbContext context, ICommissionCommandRepository _commissionCommandRepository)
        {
            _context = context;
            commissionCommandRepository = _commissionCommandRepository;
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
                    var customerIdParam = new SqlParameter("@CustomerID", payment.CustomerID);
                    var policyIdParam = new SqlParameter("@PolicyID", payment.PolicyID);
                    var amountParam = new SqlParameter("@Amount", payment.Amount);
                    var paymentDateParam = new SqlParameter("@PaymentDate", payment.PaymentDate);

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC ValidatePayment @CustomerID, @PolicyID, @Amount, @PaymentDate",
                        customerIdParam, policyIdParam, amountParam, paymentDateParam);

                    _context.Payments.Add(payment);

                    CommissionModel commissionModel = new CommissionModel
                    {
                        AgentID = payment.Customer.AgentID,
                        PolicyID = payment.PolicyID,
                        Amount = payment.Amount,
                    };
                    await commissionCommandRepository.AddCommissionAsync(commissionModel);

                    await _context.SaveChangesAsync();
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
