using Application.Heroes.Queries.GetHeroes;

namespace Application.UnitTests.Heroes.Queries
{
    public class GetHeroesTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_ReturnAllHeroes()
        {
            var context = ContextInitialize();
            var handler = new GetHeroesQueryHandler(context.Object);
            var request = new GetHeroesQuery();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotEmpty(result.Heroes);
            Assert.True(0 < result.Heroes.Count);
            Assert.NotEqual(0, result.Heroes.First().HeroId);
            Assert.NotEmpty(result.Heroes.First().Name);
            Assert.NotEqual(0, result.Heroes.First().Level);
            Assert.NotEqual(0, result.Heroes.First().JobId);
            Assert.NotNull(result.Heroes.First().Job);
            Assert.True(0 < result.Heroes.First().Mounts.Count);
        }
    }
}
