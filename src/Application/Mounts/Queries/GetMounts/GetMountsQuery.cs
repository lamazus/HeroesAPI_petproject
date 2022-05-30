
namespace Application.Mounts.Queries.GetMounts
{
    public class GetMountsQuery : IRequest<MountsVm>
    {

    }

    public class GetMountsQueryHandler : IRequestHandler<GetMountsQuery, MountsVm>
    {
        private readonly IApplicationDbContext _context;
        public GetMountsQueryHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<MountsVm> Handle(GetMountsQuery request, CancellationToken cancellationToken)
        {
            return new MountsVm
            {
                Mounts = await _context.Mounts
                .Include(p => p.Heroes)
                .ThenInclude(p=>p.Job)
                .ToListAsync()
            };
        }
    }
}
