﻿using InsuranceMLL.AdminModels;
using UserModelLayer;

namespace InsuranceAppBLL.AdminService
{
    public interface IAdminService
    {
        Task CreateAdminAsync(AdminRegistrationModel admin);
    }

}