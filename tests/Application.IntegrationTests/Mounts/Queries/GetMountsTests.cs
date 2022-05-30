using Application.Mounts.Queries.GetMounts;


namespace Application.UnitTests.Mounts.Queries
{
    public class GetMountsTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_ReturnAllMounts()
        {
            var context = ContextInitialize();
            var handler = new GetMountsQueryHandler(context.Object);
            var request = new GetMountsQuery();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotEmpty(result.Mounts);
            Assert.True(0 < result.Mounts.Count);
            Assert.NotEqual(0, result.Mounts.First().MountId);
            Assert.NotEmpty(result.Mounts.First().Name);
        }
    }
}
