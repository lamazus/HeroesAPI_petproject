using Application.Mounts.Commands.UpdateMount;

namespace Application.UnitTests.Mounts.Commands
{
    public class UpdateMountTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_UpdateMount_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new UpdateMoundCommandHandler(context.Object);
            var request = new UpdateMountCommand { MountId = 1, Name = "Turtle", Speed = 2 };

            await handler.Handle(request, CancellationToken.None);

            var editedEntity = await context.Object.Mounts.SingleOrDefaultAsync(x => x.MountId == request.MountId);

            Assert.Equal(request.MountId, editedEntity?.MountId);
            Assert.Equal(request.Name, editedEntity?.Name);
        }

        [Fact]
        public async Task Should_ThrowException_When_MountWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new UpdateMoundCommandHandler(context.Object);
            var request = new UpdateMountCommand { MountId = 5, Name = "updatedMount", Speed= 4 };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public void Should_HaveError_When_InputIsNotValid()
        {
            var validator = new UpdateMountCommandValidator();
            var request = new UpdateMountCommand { MountId = 1, Name = "", Speed = 10 };

            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(request => request.Name);
            result.ShouldNotHaveValidationErrorFor(request => request.Speed);
        }
    }
}
