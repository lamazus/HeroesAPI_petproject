using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mounts.Commands.UpdateMount
{
    public class UpdateMountCommand : IRequest
    {
        public int MountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Speed { get; set; }
    }

    public class UpdateMoundCommandHandler : IRequestHandler<UpdateMountCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateMoundCommandHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<Unit> Handle(UpdateMountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Mounts.SingleOrDefaultAsync(x => x.MountId == request.MountId, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Mount), request.MountId);

            entity.Name = request.Name;
            entity.Speed = request.Speed;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
