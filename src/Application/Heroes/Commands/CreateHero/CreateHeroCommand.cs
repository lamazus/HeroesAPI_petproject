
namespace Application.Heroes.Commands.CreateHero
{
    public class CreateHeroCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int JobId { get; set; }

    }

    public class CreateHeroCommandHandler : IRequestHandler<CreateHeroCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateHeroCommandHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<int> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
        {
            var newHero = new Hero
            {
                Name = request.Name,
                Level = request.Level,
                JobId = request.JobId
            };

            _context.Heroes.Add(newHero);
            await _context.SaveChangesAsync(cancellationToken);

            return newHero.HeroId;
        }
    }
}
