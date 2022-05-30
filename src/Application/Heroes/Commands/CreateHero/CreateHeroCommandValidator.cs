

namespace Application.Heroes.Commands.CreateHero
{
    public class CreateHeroCommandValidator : AbstractValidator<CreateHeroCommand>
    {
        public CreateHeroCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(v => v.Level)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(60);

            RuleFor(v => v.JobId)
                .GreaterThan(0)
                .NotEmpty();
        }
    }
}
