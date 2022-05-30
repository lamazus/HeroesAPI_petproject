using Application.Mounts.Queries.GetMount;

namespace Application.UnitTests.Mounts.Queries
{
    public class GetMountTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_ReturnsMount_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new GetMountQueryHandler(context.Object);
            var request = new GetMountQuery { MountId = 1 };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_ThrowException_When_MountIdWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new GetMountQueryHandler(context.Object);
            var request = new GetMountQuery { MountId = 5 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
