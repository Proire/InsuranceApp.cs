using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModelLayer;

namespace InsuranceAppRLL.Repositories.Interfaces.AdminRepository
{
    public interface IAdminCommandRepository
    {
        Task RegisterAdminAsync(Admin admin);
    }

}
