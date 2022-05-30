using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Heroes.Commands.HeroAddMount
{
    public class HeroAddMountCommand : IRequest
    {
        public int HeroId { get; set; }
        public int MountId { get; set; }
    }

    public class HeroAddMountCommandHandler : IRequestHandler<HeroAddMountCommand>
    {
        private readonly IApplicationDbContext _context;
        public HeroAddMountCommandHandler(IApplicationDbContext context)
            => _context = context;
        public async Task<Unit> Handle(HeroAddMountCommand request, CancellationToken cancellationToken)
        {
            var selectedHero = await _context.Heroes.SingleOrDefaultAsync(x=>x.HeroId == request.HeroId, cancellationToken);
            if (selectedHero == null)
                throw new NotFoundException(nameof(Hero), request.HeroId);

            var selectedMount = await _context.Mounts.SingleOrDefaultAsync(x=>x.MountId == request.MountId, cancellationToken);
            if (selectedMount == null)
                throw new NotFoundException(nameof(Mount), request.HeroId);

            if (selectedHero.Mounts.Contains(selectedMount))
                throw new Exception($"{selectedMount.Name} is already exists");

            selectedHero.Mounts.Add(selectedMount);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
