using Application.Heroes.Commands.HeroRemoveMount;

namespace Application.UnitTests.Heroes.Commands
{
    public class HeroRemoveMountTests : ApplicationContextMock
    {
        [Fact]
        public async void Should_RemoveMountFromHero_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new HeroRemoveMountCommandHandler(context.Object);
            var request = new HeroRemoveMountCommand { HeroId = 1, MountId = 1 };

            await handler.Handle(request, CancellationToken.None);
            var editedEntity = await context.Object.Heroes.SingleOrDefaultAsync(x => x.HeroId == request.HeroId);

            Assert.Empty(editedEntity?.Mounts);
        }

        [Fact]
        public async void Should_ThrowException_When_HeroWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new HeroRemoveMountCommandHandler(context.Object);
            var request = new HeroRemoveMountCommand { HeroId = 11, MountId = 1 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_When_MountWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new HeroRemoveMountCommandHandler(context.Object);
            var request = new HeroRemoveMountCommand { HeroId = 1, MountId = 5 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
