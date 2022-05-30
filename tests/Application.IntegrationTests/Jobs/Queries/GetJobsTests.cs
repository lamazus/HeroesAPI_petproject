using Application.Jobs.Queries.GetJobs;


namespace Application.UnitTests.Jobs.Queries
{
    public class GetJobsTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_ReturnAllJobs()
        {
            var context = ContextInitialize();
            var handler = new GetJobsQueryHandler(context.Object);
            var request = new GetJobsQuery();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotEmpty(result.Jobs);
            Assert.True(0 < result.Jobs.Count);
            Assert.NotEqual(0, result.Jobs.First().JobId);
            Assert.NotEmpty(result.Jobs.First().Name);
        }
    }
}
