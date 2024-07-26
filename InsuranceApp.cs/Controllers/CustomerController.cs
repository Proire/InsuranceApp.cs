using InsuranceAppBLL.CustomerService;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceMLL.CustomerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [Authorize(AuthenticationSchemes = "InsuranceAgentScheme", Roles = "InsuranceAgent")]
        [HttpPost]
        [Route("customer_user/register")]
        public async Task<ActionResult<ResponseModel<Customer>>> CreateCustomer([FromBody] CustomerRegistrationModel customer)
        {
            try
            {
                int agentId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                await _customerService.RegisterCustomerAsync(customer,agentId);

                // Log the registration event
               // _logger.LogInformation($"Customer {customer.Email} registered.");

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

        [Authorize(AuthenticationSchemes = "InsuranceAgentScheme", Roles = "InsuranceAgent")]
        [HttpGet]
        [Route("customer_user/agent/customers")]
        public async Task<ResponseModel<IEnumerable<Customer>>> GetCustomersByAgentIdAsync()
        {
            try
            {
                int agentId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                // Call the repository method to get the customers
                var customers = await _customerService.GetCustomers(agentId);

                // Return the response model with success status
                return new ResponseModel<IEnumerable<Customer>>
                {
                    Data = customers,
                    Message = "Customers retrieved successfully.",
                    Status = true
                };
            }
            catch (CustomerException ex)
            {
                // Handle specific customer exceptions
                return new ResponseModel<IEnumerable<Customer>>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                return new ResponseModel<IEnumerable<Customer>>
                {
                    Message = "An unexpected error occurred. "+ex.Message,
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "InsuranceAgentScheme", Roles = "InsuranceAgent")]
        [HttpPut("customer_user/{customerId}")]
        public async Task<ActionResult<ResponseModel<string>>> UpdateCustomer([FromBody] CustomerUpdateModel customerUpdateModel, int customerId)
        {
            try
            {
                int agentId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                await _customerService.UpdateCustomerAsync(customerUpdateModel, customerId, agentId);
                return new ResponseModel<string>
                {
                    Message = "Customer updated successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "InsuranceAgentScheme", Roles = "InsuranceAgent")]
        [HttpDelete("customer_user/{customerId}")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteCustomer(int customerId)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(customerId);
                return new ResponseModel<string>
                {
                    Message = "Customer deleted successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "InsuranceAgentScheme", Roles = "InsuranceAgent")]
        [HttpGet("customer_user/{customerId}")]
        public async Task<ActionResult<ResponseModel<Customer>>> GetCustomerById(int customerId)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(customerId);
                return new ResponseModel<Customer>
                {
                    Data = customer,
                    Message = "Customer retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<Customer>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpGet("customer_user/Customers")]
        public async Task<ActionResult<ResponseModel<IEnumerable<Customer>>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return new ResponseModel<IEnumerable<Customer>>
                {
                    Data = customers,
                    Message = "Customers retrieved successfully.",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<Customer>>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }
}
