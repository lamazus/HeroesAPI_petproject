
namespace Application.Jobs.Queries.GetJobs
{
    public class GetJobsQuery : IRequest<JobsVm>
    {

    }

    public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, JobsVm>
    {
        private readonly IApplicationDbContext _context;

        public GetJobsQueryHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<JobsVm> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            return new JobsVm
            {
                Jobs = await _context.Jobs
                .Include(p => p.Heroes)
                .ToListAsync()
            };
        }
    }
}
