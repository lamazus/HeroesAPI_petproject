
namespace Application.Jobs.Commands.CreateJob
{
    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        public CreateJobCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
