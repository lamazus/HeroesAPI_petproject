
namespace Application.Mounts.Queries.GetMount
{
    public class GetMountQuery : IRequest<MountVm>
    {
        public int MountId { get; set; }
    }

    public class GetMountQueryHandler : IRequestHandler<GetMountQuery, MountVm>
    {
        private readonly IApplicationDbContext _context;
        public GetMountQueryHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<MountVm> Handle(GetMountQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Mounts
                .Include(p=>p.Heroes)
                .SingleOrDefaultAsync(p => p.MountId == request.MountId);

            if (entity == null)
                throw new NotFoundException(nameof(Hero), request.MountId);

            return new MountVm
            {
                Name = entity.Name,
                Speed = entity.Speed,
                Heroes = entity.Heroes
            };
        }
    }
}
