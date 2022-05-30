
namespace Application.Heroes.Commands.UpdateHero
{
    public class UpdateHeroCommand : IRequest
    {
        public int HeroId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int JobId { get; set; }
    }

    public class UpdateHeroCommandHandler : IRequestHandler<UpdateHeroCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateHeroCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<Unit> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Heroes.SingleOrDefaultAsync(x => x.HeroId == request.HeroId);

            if (entity == null)
                throw new NotFoundException(nameof(Hero), request.HeroId);

            entity.Name = request.Name;
            entity.Level = request.Level;
            entity.JobId = request.JobId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
