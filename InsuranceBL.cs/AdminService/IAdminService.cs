using InsuranceAppRLL.Entities;
using InsuranceMLL.AdminModels;
using UserModelLayer;

namespace InsuranceAppBLL.AdminService
{
    public interface IAdminService
    {
        Task CreateAdminAsync(AdminRegistrationModel admin);

        Task DeleteAdminAsync(int adminId);

        Task UpdateAdminAsync(AdminUpdateModel admin, int adminId);

        Task<IEnumerable<Admin>> GetAllAdminsAsync();

        Task<Admin> GetAdminByIdAsync(int adminId);
    }

}
