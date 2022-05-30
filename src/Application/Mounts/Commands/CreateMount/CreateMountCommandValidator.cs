
namespace Application.Mounts.Commands.CreateMount
{
    public class CreateMountCommandValidator : AbstractValidator<CreateMountCommand>
    {
        public CreateMountCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(v => v.Speed)
                .NotEmpty()
                .LessThan(11)
                .GreaterThan(0);
        }
    }
}
