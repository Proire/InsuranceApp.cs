using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.AdminRepository
{
    public interface IAdminQueryRepository
    {
        Task<IEnumerable<Admin>> GetAllAdminsAsync();

        Task<Admin> GetAdminByIdAsync(int adminId);
    }
}
