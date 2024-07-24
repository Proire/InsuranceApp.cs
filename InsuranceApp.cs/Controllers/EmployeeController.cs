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

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpPut("/update/{employeeId}")]
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
        [HttpDelete("/delete/{employeeId}")]
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

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpGet("/{employeeId}")]
        public async Task<ResponseModel<Employee>> GetEmployeeById(int employeeId)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
                if (employee == null)
                {
                    return new ResponseModel<Employee>
                    {
                        Data = new Employee(),
                        Message = $"No employee found with id: {employeeId}",
                        Status = false
                    };
                }
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
                    Data = new Employee(),
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpGet("/employees")]
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
                    Data = new List<Employee>(),
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }
}
