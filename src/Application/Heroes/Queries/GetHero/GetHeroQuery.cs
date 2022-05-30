using Microsoft.EntityFrameworkCore;

namespace Application.Heroes.Queries.GetHero
{
    public class GetHeroQuery : IRequest<HeroVm>
    {
        public int HeroId { get; set; }
    }

    public class GetHeroQueryHandler : IRequestHandler<GetHeroQuery, HeroVm>
    {
        private readonly IApplicationDbContext _context;
        public GetHeroQueryHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<HeroVm> Handle(GetHeroQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Heroes
                .Include(p => p.Job)
                .Include(p => p.Mounts)
                .SingleOrDefaultAsync(p => p.HeroId == request.HeroId);

            if (entity == null)
                throw new NotFoundException(nameof(Hero), request.HeroId);

            return new HeroVm
            {
                HeroId = entity.HeroId,
                Name = entity.Name,
                Level = entity.Level,
                Job = entity.Job,
                Mounts = entity.Mounts
            };
        }
    }
}
