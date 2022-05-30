using Application.Jobs.Commands.UpdateJob;

namespace Application.UnitTests.Jobs.Commands
{
    public class UpdateJobTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_UpdateJob_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new UpdateJobCommandHandler(context.Object);
            var request = new UpdateJobCommand { JobId = 1, Name = "updatedJob" };

            await handler.Handle(request, CancellationToken.None);
            var editedEntity = await context.Object.Jobs.SingleOrDefaultAsync(x => x.JobId == request.JobId);

            Assert.Equal(request.JobId, editedEntity?.JobId);
            Assert.Equal(request.Name, editedEntity?.Name);
        }

        [Fact]
        public async Task Should_ThrowException_When_JobWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new UpdateJobCommandHandler(context.Object);
            var request = new UpdateJobCommand { JobId = 4, Name = "updatedJob" };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public void Should_HaveError_When_InputIsNotValid()
        {
            var validator = new UpdateJobCommandValidator();
            var request = new UpdateJobCommand{ JobId = 1, Name = "" };

            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(request => request.Name);
         }
    }
}
