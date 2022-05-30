using Moq;
using Moq.EntityFrameworkCore;
using Xunit;
using Web.Services;
using Application.Common.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Web.UnitTests
{
    public class AuthServiceTests
    {
        readonly Mock<IApplicationDbContext> context;
        readonly Mock<AuthenticationService> authService;

        public AuthServiceTests()
        {
            context = new Mock<IApplicationDbContext>();
            authService = new Mock<AuthenticationService>(context.Object);
        }
        [Fact]
        void EncryptPassword_SetsHashAndSaltValues()
        {
            authService.Object.EncryptPassword("myPass", out byte[] hash, out byte[] salt);

            Assert.Equal(64, hash.Length);
            Assert.Equal(128, salt.Length);
        }

        [Fact]
        void VerifyPassword_HashAndSaltAreExists_True()
        {
            authService.Object.EncryptPassword("myPass", out byte[] hash, out byte[] salt);

            bool result = authService.Object.VerifyPassword("myPass", hash, salt);
            bool wrontInput = authService.Object.VerifyPassword("wrongPass", hash, salt);

            Assert.True(result);
            Assert.False(wrontInput);
        }

        [Fact]
        async Task UserExists_UserWasFoundInDatabase_True()
        {
            authService.Object.EncryptPassword("myPass", out byte[] hash, out byte[] salt);
            List<User> users = new List<User>()
            {
                new User
                {
                    UserId = System.Guid.NewGuid(),
                    Role = UserRoles.user,
                    Username = "testUser",
                    PasswordHash = hash,
                    PasswordSalt = salt,
                }
            };
         
            context.Setup(e => e.Users).ReturnsDbSet(users);

            Assert.True(await authService.Object.IsUsernameExists("testUser"));
            Assert.False(await authService.Object.IsUsernameExists("randomName"));
        }
    }
}
