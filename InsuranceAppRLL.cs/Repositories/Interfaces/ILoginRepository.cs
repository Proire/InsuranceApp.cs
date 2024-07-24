using System.Threading.Tasks;
using InsuranceMLL;

namespace InsuranceAppRLL.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> LoginAsync(LoginModel model);
    }
}
