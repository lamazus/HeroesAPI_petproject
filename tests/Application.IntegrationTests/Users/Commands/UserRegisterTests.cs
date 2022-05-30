using Application.Users.Commands.UserRegister;
using Web.Services;
using Moq;
using Domain.Entities;
using System;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests.Users.Commands
{
    public class UserRegisterTests : ApplicationContextMock
    {
        [Fact]
        public async Task Should_CreateNewUser_When_InputIsValid()
        {
            var context = ContextInitialize();
            var auth = new Mock<AuthenticationService>(context.Object);
            var handler = new UserRegisterCommandHandler(context.Object, auth.Object);
            var request = new UserRegisterCommand { Username = "newUser", Email = "newUser@api.com", Password = "easyPassword" };

            await handler.Handle(request, CancellationToken.None);

            context.Verify(x => x.Users.Add(It.IsAny<User>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(CancellationToken.None));
        }

        [Fact]
        public async Task Should_ThrowException_When_UsernameAlreadyExists ()
        {
            var context = ContextInitialize();
            var auth = new Mock<AuthenticationService>(context.Object);
            var handler = new UserRegisterCommandHandler(context.Object, auth.Object);
            var request = new UserRegisterCommand { Username = "admin", Email = "user@api.com", Password = "easyPassword" };

            await Assert.ThrowsAsync<UserRegistrationException>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Should_ThrowException_When_EmailAlreadyExists()
        {
            var context = ContextInitialize();
            var auth = new Mock<AuthenticationService>(context.Object);
            var handler = new UserRegisterCommandHandler(context.Object, auth.Object);
            var request = new UserRegisterCommand { Username = "user", Email = "admin@api.com", Password = "easyPassword" };

            await Assert.ThrowsAsync<UserRegistrationException>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public void Should_HaveError_When_InputIsNotValid()
        {
            var validator = new UserRegisterCommandValidator();
            var request = new UserRegisterCommand { Username = "ValidName", Email = "s@ru", Password = "" };

            var response = validator.TestValidate(request);
            response.ShouldNotHaveValidationErrorFor(request => request.Username);
            response.ShouldHaveValidationErrorFor(request => request.Email);
            response.ShouldHaveValidationErrorFor(request => request.Password);
        }
    }
}
