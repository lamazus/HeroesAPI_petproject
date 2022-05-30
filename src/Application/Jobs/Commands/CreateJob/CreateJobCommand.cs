
namespace Application.Jobs.Commands.CreateJob
{
    public class CreateJobCommand : IRequest<int>
    {
        public string Name { get; set; } = String.Empty;
    }

    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateJobCommandHandler(IApplicationDbContext context)
            => _context = context;

        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var entity = new Job
            {
                Name = request.Name
            };

            _context.Jobs.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.JobId;
        }
    }
}
