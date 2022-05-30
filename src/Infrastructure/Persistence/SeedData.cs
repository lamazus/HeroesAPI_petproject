using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class SeedData
    {
        public static async Task Initialize(IApplicationDbContext context, IAuthenticationService authService)
        {
            if (!context.Heroes.Any())
            {

                authService.EncryptPassword("admin", out byte[] adminHash, out byte[] adminSalt);
                authService.EncryptPassword("userPass", out byte[] userHash, out byte[] userSalt);

                context.Jobs.AddRange(
                    new Job { Name = "Assassin" },
                    new Job { Name = "Berserk" }
                    );
                await context.SaveChangesAsync(CancellationToken.None);

                context.Mounts.AddRange(
                    new Mount { Name = "Dragon", Speed = 10 },
                    new Mount { Name = "Turtle", Speed = 3 }
                    );
                await context.SaveChangesAsync(CancellationToken.None);

                var job1 = context.Jobs.Single(x=>x.Name == "Assassin");
                var job2 = context.Jobs.Single(x => x.Name == "Berserk");

                var mount1 = context.Mounts.Single(x => x.Name == "Dragon");
                var mount2 = context.Mounts.Single(x => x.Name == "Turtle");

                context.Heroes.AddRange(
                    new Hero { Name = "HolyKiller", Level = 30, JobId = job1.JobId, Job = job1, Mounts = { mount1 } },
                    new Hero { Name = "BloodAxe", Level = 27, JobId = job2.JobId, Job = job2, Mounts = { mount1, mount2 } }
                    );
                await context.SaveChangesAsync(CancellationToken.None);

                context.Users.AddRange(
                    new User { Username = "admin", Email = "admin@mail.ru", Role = UserRoles.admin, PasswordHash = adminHash, PasswordSalt = adminSalt },
                    new User { Username = "user", Email = "user@mail.ru", Role = UserRoles.user, PasswordHash = userHash, PasswordSalt = userSalt }
                    );
                await context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}

