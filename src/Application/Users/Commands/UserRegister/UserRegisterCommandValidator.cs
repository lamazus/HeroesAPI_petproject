

namespace Application.Users.Commands.UserRegister
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(20)
                .Matches(@"[\w]");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20)
                .Matches(@"[\w\W]");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(30)
                .Matches(@"[\w]+@[a-zA-Z]+\.[a-zA-Z]+[\.a-zA-Z]+$");
        }
    }
}
