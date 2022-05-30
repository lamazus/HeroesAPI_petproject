
namespace Application.Mounts.Commands.DeleteMount
{
    public class DeleteMountCommand : IRequest
    {
        public int MountId { get; set; }
    }
    public class DeleteMoundCommandHandler : IRequestHandler<DeleteMountCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteMoundCommandHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<Unit> Handle(DeleteMountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Mounts.SingleOrDefaultAsync(x => x.MountId == request.MountId, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Mount), request.MountId);

            _context.Mounts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
