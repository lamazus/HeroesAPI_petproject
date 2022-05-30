using Application.Heroes.Commands.HeroAddMount;

namespace Application.UnitTests.Heroes.Commands
{
    public class HeroAddMountTests : ApplicationContextMock
    {
        [Fact]
        public async void Should_AddMountToHero_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new HeroAddMountCommandHandler(context.Object);
            var request = new HeroAddMountCommand { HeroId = 1, MountId = 2 };

            await handler.Handle(request, CancellationToken.None);
            var editedEntity = await context.Object.Heroes.SingleOrDefaultAsync(x => x.HeroId == request.HeroId);

            Assert.Equal(2, editedEntity?.Mounts.Count());
        }

        [Fact]
        public async void Should_ThrowException_When_HeroWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new HeroAddMountCommandHandler(context.Object);
            var request = new HeroAddMountCommand { HeroId = 11, MountId = 1 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_When_MountWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new HeroAddMountCommandHandler(context.Object);
            var request = new HeroAddMountCommand { HeroId = 1, MountId = 5 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
