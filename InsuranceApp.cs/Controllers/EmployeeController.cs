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
        [Route("employee_user/register")]
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

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpPut("employee_user/update/{employeeId}")]
        public async Task<ResponseModel<string>> UpdateEmployee([FromBody] EmployeeUpdateModel employeeModel, int employeeId)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(employeeModel, employeeId);
                return new ResponseModel<string>
                {
                    Message = "Employee updated successfully",
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

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpDelete("employee_user/delete/{employeeId}")]
        public async Task<ResponseModel<string>> DeleteEmployee(int employeeId)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(employeeId);
                return new ResponseModel<string>
                {
                    Message = "Employee deleted successfully",
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

        [Authorize(AuthenticationSchemes = "EmployeeScheme", Roles = "Employee")]
        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpGet("employee_user/{employeeId}")]
        public async Task<ResponseModel<Employee>> GetEmployeeById(int employeeId)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
                return new ResponseModel<Employee>
                {
                    Data = employee,
                    Message = "Employee retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<Employee>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpGet("employee_user/employees")]
        public async Task<ResponseModel<IEnumerable<Employee>>> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return new ResponseModel<IEnumerable<Employee>>
                {
                    Data = employees,
                    Message = "Employees retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<Employee>>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }
}
