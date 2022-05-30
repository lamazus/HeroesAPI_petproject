
namespace Application.Users.Commands.UserLogin
{
    public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator()
        {
            RuleFor(v => v.Username)
                .NotEmpty();

            RuleFor(v => v.Password)
                .MaximumLength(15)
                .NotEmpty();

        }
    }
}
