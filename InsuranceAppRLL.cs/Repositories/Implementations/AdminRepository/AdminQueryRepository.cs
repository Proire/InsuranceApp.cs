using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAppRLL.Repositories.Implementations.AdminRepository
{
    public class AdminQueryRepository : IAdminQueryRepository
    {
        private readonly InsuranceDbContext _context;

        public AdminQueryRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetAdminByIdAsync(int adminId)
        {
            try
            {
                var admin = await _context.GetAdminByIdAsync(adminId);
                if (admin == null)
                {
                    throw new AdminException("Admin not found");
                }

                return admin;
            }
            catch (SqlException ex)
            {
                throw new AdminException("An error occurred while retrieving the Admin.", ex);
            }
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            try
            {
                var admins = await _context.GetAllAdminsAsync();
                if (!admins.Any())
                {
                    throw new AdminException("No Admins found.");
                }

                return admins;
            }
            catch (SqlException ex)
            {
                throw new AdminException("An error occurred while retrieving the Admins.", ex);
            }
        }
    }
}
