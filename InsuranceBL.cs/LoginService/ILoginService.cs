using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceMLL;

namespace InsuranceAppBLL.LoginService
{
    public interface ILoginService
    {
        Task<string> LoginAsync(LoginModel login);
    }
}
