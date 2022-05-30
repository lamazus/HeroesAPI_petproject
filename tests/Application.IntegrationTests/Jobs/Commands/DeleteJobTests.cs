using Application.Jobs.Commands.DeleteJob;
using Moq;
using Domain.Entities;

namespace Application.UnitTests.Jobs.Commands
{
    public class DeleteJobTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_DeleteJob_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new DeleteJobCommandHandler(context.Object);
            var request = new DeleteJobCommand { JobId = 1 };

            await handler.Handle(request, CancellationToken.None);

            context.Verify(a => a.Jobs.Remove(It.IsAny<Job>()));
            context.Verify(a => a.SaveChangesAsync(CancellationToken.None));

        }

        [Fact]
        public async Task Should_ThrowException_When_JobWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new DeleteJobCommandHandler(context.Object);
            var request = new DeleteJobCommand { JobId = 5 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
