using Application.Heroes.Commands.DeleteHero;
using Moq;
using Domain.Entities;

namespace Application.UnitTests.Heroes.Commands
{
    public class DeleteHeroTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_DeleteHero_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new DeleteHeroCommandHandler(context.Object);
            var request = new DeleteHeroCommand { HeroId = 1 };

            await handler.Handle(request, CancellationToken.None);

            context.Verify(a => a.Heroes.Remove(It.IsAny<Hero>()));
            context.Verify(a=>a.SaveChangesAsync(CancellationToken.None));

        }

        [Fact]
        public async Task Should_ThrowException_When_HeroWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new DeleteHeroCommandHandler(context.Object);
            var request = new DeleteHeroCommand { HeroId = 11 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
