using System.Threading.Tasks;
using UserModelLayer;

namespace InsuranceAppRLL.Repositories.Interfaces.AdminRepository
{
    public interface IAdminQueryRepository
    {
        Task<string> LoginAdminAsync(LoginModel model);
    }
}
