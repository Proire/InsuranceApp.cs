using InsuranceAppBLL.CustomerService;
using InsuranceAppRLL.Entities;
using InsuranceMLL.CustomerModels;
using InsuranceMLL.CustomerModels.InsuranceMLL.CustomerModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModelLayer;

namespace InsuranceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ResponseModel<Customer>>> CreateCustomer([FromBody] CustomerRegistrationModel customer)
        {
            try
            {
                await _customerService.RegisterCustomerAsync(customer);

                // Log the registration event
                _logger.LogInformation($"Customer {customer.Email} registered.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<Customer>
                {
                    Message = "Customer Added Successfully",
                    Status = true
                };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                // Handle the exception and prepare the response model
                var responseModel = new ResponseModel<Customer>
                {
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(responseModel);
            }
        }
    }

}
