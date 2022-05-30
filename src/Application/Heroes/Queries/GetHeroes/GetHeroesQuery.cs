
namespace Application.Heroes.Queries.GetHeroes
{
    public class GetHeroesQuery : IRequest<HeroesVm>
    {

    }

    public class GetHeroesQueryHandler : IRequestHandler<GetHeroesQuery, HeroesVm>
    {
        private readonly IApplicationDbContext _context;
        public GetHeroesQueryHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<HeroesVm> Handle(GetHeroesQuery request, CancellationToken cancellationToken)
        {
            return new HeroesVm
            {
                Heroes = await _context.Heroes
                .Include(p => p.Job)
                .Include(p => p.Mounts)
                .ToListAsync()
            };
        }
    }
}
