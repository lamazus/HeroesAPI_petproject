using Moq;
using Moq.EntityFrameworkCore;
using Application.Common.Interfaces;
using System.Collections.Generic;
using Domain.Entities;
using Web.Services;

namespace Application.UnitTests.Mocks
{
    public class ApplicationContextMock
    {

        public Mock<IApplicationDbContext> ContextInitialize()
        {
            #region SeedData
            var jobsList = new List<Job>
            {
                new Job { JobId = 1, Name = "Thief"},
                new Job { JobId = 2, Name = "Wizard"}
            };

            var mountsList = new List<Mount>
            {
                new Mount { MountId = 1, Name = "Dragon", Speed = 10},
                new Mount { MountId = 2, Name = "Horse", Speed = 5 }
            };
            var heroesList = new List<Hero>
            {
                new Hero
                {
                    HeroId = 1,
                    Level = 15,
                    JobId = 1,
                    Name = "testUser1",
                    Job = jobsList[0],
                    Mounts = { mountsList[0] }
                },
                new Hero
                {
                    HeroId = 2,
                    Level = 20,
                    JobId = 2,
                    Name = "testUser2",
                    Job = jobsList[1],
                    Mounts = { mountsList[0], mountsList[1] }
                }
            };
            #endregion

            var mockDb = new Mock<IApplicationDbContext>();
            var auth = new Mock<AuthenticationService>(mockDb.Object);

            auth.Object.EncryptPassword("password", out byte[] hash, out byte[] salt);
            var usersList = new List<User>
            {
                new User
                {
                    UserId = System.Guid.NewGuid(),
                    Username = "admin",
                    Role = UserRoles.admin,
                    Email = "admin@api.com",
                    PasswordHash = hash,
                    PasswordSalt = salt
                }
            };

            mockDb.Setup(e => e.Heroes).ReturnsDbSet(heroesList);
            mockDb.Setup(e => e.Jobs).ReturnsDbSet(jobsList);
            mockDb.Setup(e => e.Mounts).ReturnsDbSet(mountsList);
            mockDb.Setup(e => e.Users).ReturnsDbSet(usersList);
            return mockDb;
        }
    }
}
