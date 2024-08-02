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
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment), "Payment object is null.");
            }

            // Fetch the customer object from the database
            var customer = await _context.GetCustomerByIdAsync(payment.CustomerID);

            if (customer == null)
            {
                throw new ArgumentException($"No customer found with ID {payment.CustomerID}.", nameof(payment.CustomerID));
            }

            if (customer.AgentID <= 0)
            {
                throw new ArgumentException("AgentID must be greater than zero.", nameof(customer.AgentID));
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.AddPaymentAsync(payment.CustomerID,payment.PolicyID, payment.Amount);
                    // Parameters for the stored procedure
                    var customerIdParam = new SqlParameter("@CustomerID", payment.CustomerID);
                    var policyIdParam = new SqlParameter("@PolicyID", payment.PolicyID);
                    var amountParam = new SqlParameter("@Amount", payment.Amount);
                    var paymentDateParam = new SqlParameter("@PaymentDate", DateTime.UtcNow);

                    // Execute stored procedure for validation
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC ValidatePayment @CustomerID, @PolicyID, @Amount, @PaymentDate",
                        customerIdParam, policyIdParam, amountParam, paymentDateParam);

                    // Create the CommissionModel from the payment and associated customer
                    var commissionModel = new CommissionModel
                    {
                        AgentID = customer.AgentID, // Get AgentID from Customer
                        PolicyID = payment.PolicyID,
                        Amount = payment.Amount
                    };

                    // Call method to add commission
                    await commissionCommandRepository.AddCommissionAsync(commissionModel);

                    // Save changes and commit the transaction
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (SqlException ex)
                {
                    // Log and handle SQL exceptions
                    Console.WriteLine(ex.Message);
                    await transaction.RollbackAsync();
                    throw new PaymentException("An error occurred while adding the payment.", ex);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    await transaction.RollbackAsync();
                    throw new PaymentException("An unexpected error occurred while adding the payment.", ex);
                }
            }
        }


    }
}
