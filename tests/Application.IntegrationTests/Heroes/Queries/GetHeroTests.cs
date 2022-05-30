using Application.Heroes.Queries.GetHero;

namespace Application.UnitTests.Heroes.Queries
{
    public class GetHeroTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_ReturnHero_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new GetHeroQueryHandler(context.Object);
            var request = new GetHeroQuery { HeroId = 1 };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_ThrowException_When_HeroWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new GetHeroQueryHandler(context.Object);
            var request = new GetHeroQuery { HeroId = 5 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
