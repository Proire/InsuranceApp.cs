using InsuranceAppBLL.CustomerService;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceMLL.CustomerModels;
using InsuranceMLL.CustomerModels.InsuranceMLL.CustomerModels;
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

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ResponseModel<Customer>>> CreateCustomer([FromBody] CustomerRegistrationModel customer)
        {
            try
            {
                await _customerService.RegisterCustomerAsync(customer);

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

        [Authorize(AuthenticationSchemes = "AgentScheme", Roles = "Agent")]
        [HttpGet]
        [Route("/agent/customers")]
        public async Task<ResponseModel<IEnumerable<Customer>>> GetCustomersByAgentIdAsync()
        {
            try
            {
                // Extract AgentID from the JWT token
                int agentId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Call the repository method to get the customers
                var customers = await _customerService.GetCustomers(agentId);

                if (customers == null || !customers.Any())
                {
                    return new ResponseModel<IEnumerable<Customer>>
                    {
                        Message = "No customers found for the agent.",
                        Status = false
                    };
                }

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
                    Message = "An unexpected error occurred.",
                    Status = false
                };
            }
        }
    }
}
