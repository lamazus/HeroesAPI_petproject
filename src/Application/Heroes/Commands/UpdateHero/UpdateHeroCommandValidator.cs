
namespace Application.Heroes.Commands.UpdateHero
{
    public class UpdateHeroCommandValidator : AbstractValidator<UpdateHeroCommand>
    {
        public UpdateHeroCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(v => v.Level)
                .NotEmpty()
                .LessThan(60);

            RuleFor(v => v.JobId)
                .NotEmpty();
        }
    }
}
