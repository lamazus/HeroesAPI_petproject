
namespace Application.Jobs.Queries.GetJob
{
    public class GetJobQuery : IRequest<JobVm>
    {
        public int JobId { get; set; }
    }

    public class GetJobQueryHandler : IRequestHandler<GetJobQuery, JobVm>
    {
        private readonly IApplicationDbContext _context;
        public GetJobQueryHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<JobVm> Handle(GetJobQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Jobs
                .Include(p=>p.Heroes)
                .SingleOrDefaultAsync(p => p.JobId == request.JobId);

            if (entity == null)
                throw new NotFoundException(nameof(Hero), request.JobId);

            return new JobVm
            {
                Name = entity.Name,
                Heroes = entity.Heroes
            };
        }
    }
}
