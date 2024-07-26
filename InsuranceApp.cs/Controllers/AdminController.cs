using InsuranceAppBLL.AdminService;
using InsuranceAppRLL.Entities;
using InsuranceMLL.AdminModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using UserModelLayer;

namespace InsuranceApp.cs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService,ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        [HttpPost]
        [Route("admin_user/register")]
        public async Task<ActionResult<ResponseModel<string>>> CreateAdmin([FromBody] AdminRegistrationModel admin)
        {
            try
            {
                await _adminService.CreateAdminAsync(admin);

                // Log the registration event
                _logger.LogInformation($"Admin {admin.Email} registered.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<string>
                {
                    Message = "Admin Added Successfully",
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

        [HttpPut("admin_user/update/{adminId}")]
        public async Task<ResponseModel<string>> UpdateAdmin([FromBody] AdminUpdateModel adminModel, int adminId)
        {
            try
            {
                await _adminService.UpdateAdminAsync(adminModel, adminId);
                return new ResponseModel<string>
                {
                    Message = "Admin updated successfully",
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

        [HttpDelete("admin_user/delete/{adminId}")]
        public async Task<ResponseModel<string>> DeleteAdmin(int adminId)
        {
            try
            {
                await _adminService.DeleteAdminAsync(adminId);
                return new ResponseModel<string>
                {
                    Message = "Admin deleted successfully",
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

        [HttpGet("admin_user/{adminId}")]
        public async Task<ResponseModel<Admin>> GetAdminById(int adminId)
        {
            try
            {
                var admin = await _adminService.GetAdminByIdAsync(adminId);
                return new ResponseModel<Admin>
                {
                    Data = admin,
                    Message = "Admin retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<Admin>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [HttpGet("admin_user/admins")]
        public async Task<ResponseModel<IEnumerable<Admin>>> GetAllAdmins()
        {
            try
            {
                var admins = await _adminService.GetAllAdminsAsync();
                return new ResponseModel<IEnumerable<Admin>>
                {
                    Data = admins,
                    Message = "Admins retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<Admin>>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }
}
