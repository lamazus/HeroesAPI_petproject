using Application.Mounts.Commands.DeleteMount;
using Moq;
using Domain.Entities;

namespace Application.UnitTests.Mounts.Commands
{
    public class DeleteMountTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_DeleteMount_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new DeleteMoundCommandHandler(context.Object);
            var request = new DeleteMountCommand { MountId = 1 };

            await handler.Handle(request, CancellationToken.None);

            context.Verify(a => a.Mounts.Remove(It.IsAny<Mount>()));
            context.Verify(a => a.SaveChangesAsync(CancellationToken.None));

        }

        [Fact]
        public async Task Should_ThrowException_When_MountWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new DeleteMoundCommandHandler(context.Object);
            var request = new DeleteMountCommand { MountId = 5 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
