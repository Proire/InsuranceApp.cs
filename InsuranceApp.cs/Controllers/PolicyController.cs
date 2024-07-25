using InsuranceAppBLL.PolicyService;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceMLL.PolicyModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModelLayer;

namespace InsuranceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        private readonly ILogger<PolicyController> _logger;

        public PolicyController(IPolicyService policyService, ILogger<PolicyController> logger)
        {
            _policyService = policyService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/customer/{customerId}/policies")]
        public async Task<ActionResult<ResponseModel<IEnumerable<Policy>>>> GetAllPoliciesByCustomerIdAsync(int customerId)
        {
            try
            {
                // Call the service method to get all policies by customer ID
                var policies = await _policyService.GetAllPoliciesForCustomersAsync(customerId);

                if (policies == null || !policies.Any())
                {
                    return new ResponseModel<IEnumerable<Policy>>
                    {
                        Data = new List<Policy>(),
                        Message = "No policies found for the customer.",
                        Status = false
                    };
                }

                // Return the response model with success status
                return new ResponseModel<IEnumerable<Policy>>
                {
                    Data = policies,
                    Message = "Policies retrieved successfully.",
                    Status = true
                };
            }
            catch (PolicyException ex)
            {
                // Handle specific policy exceptions
                return new ResponseModel<IEnumerable<Policy>>
                {
                    Data = new List<Policy>(),
                    Message = ex.Message,
                    Status = false
                };
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                return new ResponseModel<IEnumerable<Policy>>
                {
                    Data = new List<Policy>(),
                    Message = "An unexpected error occurred. " + ex.Message,
                    Status = false
                };
            }
        }

        [HttpGet("{policyId}")]
        public async Task<ActionResult<ResponseModel<Policy>>> GetPolicyByIdAsync(int policyId)
        {
            try
            {
                var policy = await _policyService.GetPolicyByIdAsync(policyId);
                if (policy == null)
                {
                    return new ResponseModel<Policy>
                    {
                        Data = new Policy(),
                        Message = $"No policy found with id: {policyId}",
                        Status = false
                    };
                }
                return new ResponseModel<Policy>
                {
                    Data = policy,
                    Message = "Policy retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<Policy>
                {
                    Data = new Policy(),
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ResponseModel<string>>> CreatePolicy([FromBody] PolicyRegistrationModel policyModel)
        {
            try
            {
                await _policyService.CreatePolicyAsync(policyModel);

                // Log the creation event
                _logger.LogInformation($"Policy for customer {policyModel.CustomerID} created.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<string>
                {
                    Message = "Policy Created Successfully",
                    Status = true
                };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                // Handle the exception and prepare the response model
                var responseModel = new ResponseModel<string>
                {
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(responseModel);
            }
        }

        [HttpPut("/update/{policyId}")]
        public async Task<ActionResult<ResponseModel<string>>> UpdatePolicy([FromBody] PolicyUpdateModel updatePolicyModel, int policyId)
        {
            try
            {
                await _policyService.UpdatePolicyAsync(updatePolicyModel, policyId);

                // Log the update event
                _logger.LogInformation($"Policy {policyId} updated.");

                return new ResponseModel<string>
                {
                    Message = "Policy updated successfully",
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

        [HttpDelete("/delete/{policyId}")]
        public async Task<ActionResult<ResponseModel<string>>> DeletePolicy(int policyId)
        {
            try
            {
                await _policyService.DeletePolicyAsync(policyId);

                // Log the deletion event
                _logger.LogInformation($"Policy {policyId} deleted.");

                return new ResponseModel<string>
                {
                    Message = "Policy deleted successfully",
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
    }
}
