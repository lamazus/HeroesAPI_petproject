using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Hero> Heroes => Set<Hero>();
        public DbSet<Job> Jobs => Set<Job>();
        public DbSet<Mount> Mounts => Set<Mount>();
        public DbSet<User> Users => Set<User>();

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}