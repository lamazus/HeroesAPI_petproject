
namespace Application.Mounts.Commands.UpdateMount
{
    public class UpdateMountCommandValidator : AbstractValidator<UpdateMountCommand>
    {
        public UpdateMountCommandValidator()
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
