
namespace Application.Jobs.Commands.UpdateJob
{
    public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
    {
        public UpdateJobCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
