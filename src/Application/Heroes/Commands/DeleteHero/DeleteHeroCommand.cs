
namespace Application.Heroes.Commands.DeleteHero
{
    public class DeleteHeroCommand : IRequest
    {
        public int HeroId { get; set; }
    }

    public class DeleteHeroCommandHandler : IRequestHandler<DeleteHeroCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteHeroCommandHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<Unit> Handle(DeleteHeroCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Heroes.SingleOrDefaultAsync(x=>x.HeroId == request.HeroId);

            if (entity == null)
                throw new NotFoundException(nameof(Hero), request.HeroId);

            
            _context.Heroes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
