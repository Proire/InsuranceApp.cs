using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
                var book = await _context.Admins.FindAsync(adminId);
                if (book == null)
                {
                    throw new AdminException($"Admin Not found");
                }
                return book;
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
                var books = await _context.Admins.ToListAsync();
                if (books.Count == 0)
                {
                    throw new AdminException("No Admins found.");
                }
                return books;
            }
            catch (SqlException ex)
            {
                throw new AdminException("An error occurred while retrieving the Admins.", ex);
            }
        }
    }
}
