
namespace Application.Heroes.Commands.HeroRemoveMount
{
    public class HeroRemoveMountCommand : IRequest
    {
        public int HeroId { get; set; }
        public int MountId { get; set; }
    }

    public class HeroRemoveMountCommandHandler : IRequestHandler<HeroRemoveMountCommand>
    {
        private readonly IApplicationDbContext _context;
        public HeroRemoveMountCommandHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<Unit> Handle(HeroRemoveMountCommand request, CancellationToken cancellationToken)
        {
            var selectedHero = await _context.Heroes.Include(p => p.Mounts).SingleOrDefaultAsync(p => p.HeroId == request.HeroId, cancellationToken);
            if (selectedHero == null)
                throw new NotFoundException(nameof(Hero), request.HeroId);

            var selectedMount = selectedHero.Mounts.Find(p => p.MountId == request.MountId);
            if (selectedMount == null)
                throw new NotFoundException(nameof(Mount), request.MountId);

            selectedHero.Mounts.Remove(selectedMount);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
