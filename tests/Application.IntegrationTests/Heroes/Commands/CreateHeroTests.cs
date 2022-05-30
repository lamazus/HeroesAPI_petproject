using Application.Heroes.Commands.CreateHero;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Heroes.Commands
{
    public class CreateHeroTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_CreateNewHero_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new CreateHeroCommandHandler(context.Object);
            var request = new CreateHeroCommand { Name = "testUser3", Level = 25, JobId = 2 };

            await handler.Handle(request, CancellationToken.None);

            context.Verify(x => x.Heroes.Add(It.IsAny<Hero>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public void Should_HaveError_When_InputIsNotValid()
        {
            var validator = new CreateHeroCommandValidator();
            var request = new CreateHeroCommand { Name = "testUser5", JobId = 0, Level = 0 };

            var result = validator.TestValidate(request);
            result.ShouldNotHaveValidationErrorFor(request => request.Name);
            result.ShouldHaveValidationErrorFor(request => request.JobId);
            result.ShouldHaveValidationErrorFor(request => request.Level);
        }
    }
}
