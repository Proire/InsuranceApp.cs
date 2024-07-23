using System.Threading.Tasks;
using InsuranceMLL;

namespace InsuranceAppRLL.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> LoginAdminAsync(LoginModel model);
        Task<string> LoginCustomerAsync(LoginModel model);
        Task<string> LoginInsuranceAgentAsync(LoginModel model);
        Task<string> LoginEmployeeAsync(LoginModel model);
    }
}
