using InsuranceAppBLL.PaymentService;
using InsuranceAppRLL.Entities;
using InsuranceMLL.PaymentModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModelLayer;

namespace InsuranceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = "InsuranceAgentScheme", Roles = "InsuranceAgent")]
        [HttpPost("payment/create")]
        public async Task<ActionResult<ResponseModel<string>>> CreatePayment([FromBody] PaymentCreationModel paymentModel)
        {
            try
            {
                await _paymentService.CreatePaymentAsync(paymentModel);

                // Log the creation event
                _logger.LogInformation($"Payment for customer {paymentModel.CustomerID} created.");

                var responseModel = new ResponseModel<string>
                {
                    Message = "Payment created successfully",
                    Status = true
                };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                var responseModel = new ResponseModel<string>
                {
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(responseModel);
            }
        }

        [HttpGet("payments/policy/{policyId}")]
        public async Task<ActionResult<ResponseModel<IEnumerable<Payment>>>> GetPaymentsForPolicyAsync(int policyId)
        {
            try
            {
                var payments = await _paymentService.GetPaymentsForPolicyAsync(policyId);

                return new ResponseModel<IEnumerable<Payment>>
                {
                    Data = payments,
                    Message = "Payments retrieved successfully.",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<Payment>>
                {
                    Data = new List<Payment>(),
                    Message = "An unexpected error occurred. " + ex.Message,
                    Status = false
                };
            }
        }

        [HttpGet("payments/customer/{customerId}")]
        public async Task<ActionResult<ResponseModel<IEnumerable<Payment>>>> GetPaymentsForCustomerAsync(int customerId)
        {
            try
            {
                var payments = await _paymentService.GetPaymentsForCustomerAsync(customerId);

                return new ResponseModel<IEnumerable<Payment>>
                {
                    Data = payments,
                    Message = "Payments retrieved successfully.",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<Payment>>
                {
                    Data = new List<Payment>(),
                    Message = "An unexpected error occurred. " + ex.Message,
                    Status = false
                };
            }
        }
    }
}
