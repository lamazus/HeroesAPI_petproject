using Application.Jobs.Commands.CreateJob;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Jobs.Commands
{
    public class CreateJobTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_CreateNewJob_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new CreateJobCommandHandler(context.Object);
            var request = new CreateJobCommand { Name = "Hunter" };

            await handler.Handle(request, CancellationToken.None);

            context.Verify(x => x.Jobs.Add(It.IsAny<Job>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);

        }

        [Fact]
        public void Should_HaveError_When_InputIsNotValid()
        {
            var validator = new CreateJobCommandValidator();
            var request = new CreateJobCommand{ Name = "" };

            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(request => request.Name);
        }
    }
}
