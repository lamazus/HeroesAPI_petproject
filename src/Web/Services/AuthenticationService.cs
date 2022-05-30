using System.Security.Cryptography;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Web.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IApplicationDbContext _context;
        public AuthenticationService(IApplicationDbContext context)
        {
            _context = context;
        }
        public void EncryptPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computedHash.Length; i++)
                {
                    if (passwordHash[i] != computedHash[i])
                        return false;
                }

                return true;
            }
        }

        public async Task<bool> IsUsernameExists(string Username)
        {
            if (await _context.Users.AnyAsync(e => e.Username == Username))
                return true;

            return false;
        }

        public async Task<bool> IsEmailReserved(string email)
        {
            if (await _context.Users.AnyAsync(e => e.Email == email))
                return true;

            return false;
        }
        
    }
}
