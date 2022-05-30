
namespace Application.Mounts.Commands.CreateMount
{
    public class CreateMountCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public int Speed { get; set; }
    }

    public class CreateMountCommandHandler : IRequestHandler<CreateMountCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateMountCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<int> Handle(CreateMountCommand request, CancellationToken cancellationToken)
        {
            var entity = new Mount
            {
                Name = request.Name,
                Speed = request.Speed
            };

            _context.Mounts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.MountId;
        }
    }
}
