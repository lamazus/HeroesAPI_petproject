
namespace Application.Jobs.Commands.UpdateJob
{
    public class UpdateJobCommand : IRequest
    {
        public int JobId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateJobCommandHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<Unit> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Jobs
                .SingleOrDefaultAsync(x => x.JobId == request.JobId, cancellationToken); ;
            if (entity == null)
                throw new NotFoundException(nameof(Job), request.JobId);

            entity.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
