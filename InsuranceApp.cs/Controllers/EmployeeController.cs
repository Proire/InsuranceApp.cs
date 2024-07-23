using InsuranceAppBLL.EmployeeService;
using InsuranceAppRLL.Entities;
using InsuranceMLL.EmployeeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModelLayer;

namespace InsuranceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ResponseModel<Employee>>> CreateEmployee([FromBody] EmployeeRegistrationModel employee)
        {
            try
            {
                await _employeeService.RegisterEmployeeAsync(employee);

                // Log the registration event
                _logger.LogInformation($"Employee {employee.Email} registered.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<Employee>
                {
                    Message = "Employee Added Successfully",
                    Status = true
                };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                // Handle the exception and prepare the response model
                var responseModel = new ResponseModel<Employee>
                {
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(responseModel);
            }
        }
    }
}
