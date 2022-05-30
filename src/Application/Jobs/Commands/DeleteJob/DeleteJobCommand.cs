
namespace Application.Jobs.Commands.DeleteJob
{
    public class DeleteJobCommand : IRequest
    {
        public int JobId { get; set; }
    }

    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteJobCommandHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<Unit> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Jobs
                .SingleOrDefaultAsync(x => x.JobId == request.JobId, cancellationToken);

            if(entity == null)
                throw new NotFoundException(nameof(Job), request.JobId);

            _context.Jobs.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
