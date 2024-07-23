using BookStoreRL.Utilities;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModelLayer;
using UserRLL.Utilities;

namespace InsuranceAppRLL.Repositories.Implementations.AdminRepository
{
    public class AdminQueryRepository : IAdminQueryRepository
    {
        private readonly InsuranceDbContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AdminQueryRepository(InsuranceDbContext context, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> LoginAdminAsync(LoginModel model)
        {
            // Check if the admin exists asynchronously
            bool isAdmin = await _context.Admins.AnyAsync(x => x.Email == model.Email);

            if (isAdmin)
            {
                // Retrieve the admin asynchronously
                Admin? admin = await _context.Admins.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (admin != null)
                {
                    // Retrieve the key and IV for the admin
                    (byte[] key, byte[] iv) = KeyIvManager.GetKeyAndIv(admin.Email);

                    // Verify the password
                    byte[] cipheredPassword = Convert.FromBase64String(admin.Password);
                    string decryptedPassword = PasswordHasher.VerifyPassword(cipheredPassword, key, iv);

                    if (model.Password == decryptedPassword)
                    {
                        // Generate a token for the admin
                        string token = _jwtTokenGenerator.GenerateAdminToken(Convert.ToString(admin.AdminID), admin.Username, TimeSpan.FromMinutes(15));

                        return token;
                    }

                    throw new AdminException("Wrong Password, Reenter Password");
                }

                throw new AdminException("Invalid Email, Register First");
            }

            throw new AdminException("Invalid Email, Register First");
        }
    }
}
