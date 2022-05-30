
namespace Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        void EncryptPassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
        Task<bool> IsUsernameExists(string Username);
        Task<bool> IsEmailReserved(string email);
    }
}
