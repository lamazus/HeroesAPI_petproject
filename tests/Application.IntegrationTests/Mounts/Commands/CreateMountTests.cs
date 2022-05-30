using Application.Mounts.Commands.CreateMount;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Mounts.Commands
{
    public class CreateMountTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_CreateNewMount_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new CreateMountCommandHandler(context.Object);
            var request = new CreateMountCommand { Name = "Goat", Speed = 3 };

            await handler.Handle(request, CancellationToken.None);

            context.Verify(x => x.Mounts.Add(It.IsAny<Mount>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public void Should_HaveError_When_InputIsNotValid()
        {
            var validator = new CreateMountCommandValidator();
            var request = new CreateMountCommand { Name = "Cat", Speed = 100 };

            var result = validator.TestValidate(request);
            result.ShouldNotHaveValidationErrorFor(request => request.Name);
            result.ShouldHaveValidationErrorFor(request => request.Speed);
        }
    }
}
