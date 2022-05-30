using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Hero> Heroes { get; }
        DbSet<Job> Jobs { get; }
        DbSet<Mount> Mounts { get; }
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
