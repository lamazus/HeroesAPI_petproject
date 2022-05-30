using Application.Heroes.Commands.UpdateHero;

namespace Application.UnitTests.Heroes.Commands
{
    public class UpdateHeroTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_UpdateHero_When_InputIsValid()
        {
            var context = ContextInitialize();
            var handler = new UpdateHeroCommandHandler(context.Object);
            var request = new UpdateHeroCommand { HeroId = 1, Name = "updatedUser", Level = 34, JobId = 2, };

            await handler.Handle(request, CancellationToken.None);
            var editedEntity = await context.Object.Heroes.SingleOrDefaultAsync(x=>x.HeroId == request.HeroId);

            Assert.Equal(request.HeroId, editedEntity?.HeroId);
            Assert.Equal(request.Name, editedEntity?.Name);
            Assert.Equal(request.Level, editedEntity?.Level);
            Assert.Equal(request.JobId, editedEntity?.JobId);
        }

        [Fact]
        public async Task Should_ThrowException_When_HeroWasNotFound()
        {
            var context = ContextInitialize();
            var handler = new UpdateHeroCommandHandler(context.Object);
            var request = new UpdateHeroCommand { HeroId = 11, Name = "updatedUser", Level = 34, JobId = 2, };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public void Should_HaveError_When_InputIsNotValid()
        {
            var validator = new UpdateHeroCommandValidator();
            var request = new UpdateHeroCommand{ HeroId = 11, Name = "", JobId = 0, Level = 100 };

            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(request => request.Name);
            result.ShouldHaveValidationErrorFor(request => request.JobId);
            result.ShouldHaveValidationErrorFor(request => request.Level);
        }
    }
}
