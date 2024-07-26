using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.SchemeRepository
{
    public interface ISchemeCommandRepository
    {
        Task AddScheme(Scheme scheme);
        Task UpdateScheme(Scheme scheme);
        Task DeleteScheme(int schemeId);
    }
}
