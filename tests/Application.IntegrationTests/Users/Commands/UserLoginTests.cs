using Application.Users.Commands.UserLogin;
using Microsoft.Extensions.Configuration;
using Moq;
using Web.Services;

namespace Application.UnitTests.Users.Commands
{
    public class UserLoginTests : ApplicationContextMock
    {
        [Fact]
        public async void Should_ReturnJWT()
        {
            var context = ContextInitialize();
            var config = new Mock<IConfiguration>();
            var auth = new Mock<AuthenticationService>(context.Object);
            config.Setup(x => x.GetSection(It.IsAny<string>()).Value).Returns("superUltraSecretKey2022");
            var handler = new UserLoginCommandHandler(context.Object, auth.Object, config.Object);
            var request = new UserLoginCommand { Username = "admin", Password = "password" };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.IsType<string>(response);
            Assert.NotEmpty(response);
        }

        [Fact]
        public async void Should_ThrowExecption_When_UsernameNotFound()
        {
            var context = ContextInitialize();
            var config = new Mock<IConfiguration>();
            var auth = new Mock<AuthenticationService>(context.Object);
            config.Setup(x => x.GetSection(It.IsAny<string>()).Value).Returns("superUltraSecretKey2022");
            var handler = new UserLoginCommandHandler(context.Object, auth.Object, config.Object);
            var request = new UserLoginCommand { Username = "user", Password = "password" };

            await Assert.ThrowsAsync<AuthenticationErrorException>(async () => await handler.Handle(request, CancellationToken.None));

        }

        [Fact]
        public async void Should_ThrowException_When_PasswordDoesNotMatch()
        {
            var context = ContextInitialize();
            var config = new Mock<IConfiguration>();
            var auth = new Mock<AuthenticationService>(context.Object);
            config.Setup(x => x.GetSection(It.IsAny<string>()).Value).Returns("superUltraSecretKey2022");
            var handler = new UserLoginCommandHandler(context.Object, auth.Object, config.Object);
            var request = new UserLoginCommand { Username = "admin", Password = "wrongPass" };

            await Assert.ThrowsAsync<AuthenticationErrorException>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public void Should_HaveError_When_InputIsNotValid()
        {
            var validator = new UserLoginCommandValidator();
            var request = new UserLoginCommand { Username = " ", Password = "myVerySuperUltraLongPassword" };

            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(request => request.Username);
            result.ShouldHaveValidationErrorFor(request => request.Password);
        }
    }
}
