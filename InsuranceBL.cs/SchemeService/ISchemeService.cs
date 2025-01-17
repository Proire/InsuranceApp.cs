﻿using InsuranceAppRLL.Entities;
using InsuranceMLL.SchemeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.SchemeService
{
    public interface ISchemeService
    {
        Task<IEnumerable<Scheme>> GetAllSchemesForPlanAsync(int planId);

        Task<Scheme> GetSchemeByIdAsync(int schemeId);

        Task CreateSchemeAsync(SchemeRegistrationModel scheme);

        Task UpdateSchemeAsync(SchemeUpdateModel scheme, int schemeId);

        Task DeleteSchemeAsync(int schemeId);
    }
}
