using Application.Jobs.Queries.GetJob;

namespace Application.UnitTests.Jobs.Queries
{
    public class GetJobTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_ReturnJob_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new GetJobQueryHandler(context.Object);
            var request = new GetJobQuery { JobId = 1 };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_ThrowException_When_JobWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new GetJobQueryHandler(context.Object);
            var request = new GetJobQuery { JobId = 5 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
