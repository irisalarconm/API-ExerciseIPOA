using FluentValidation;
using Models;

namespace API.Validators
{
    public class ExerciseValidator : AbstractValidator<Exercise>
    {
        ILogger<ExerciseValidator> _logger;
        public ExerciseValidator(ILogger<ExerciseValidator> logger)
        {
            _logger = logger;

            RuleFor(exercise => exercise.Name).NotEmpty().WithMessage("This field is required");
            RuleFor(exercise => exercise.Name).MaximumLength(75).WithMessage("This field is can not has more than 150 characters");

            RuleFor(exercise => exercise.Description).NotEmpty().WithMessage("This field is required");
            //RuleFor(exercise => exercise.Data).NotEmpty().WithMessage("This field is required");
            // RuleFor(client => client.DNIClient).GreaterThan(0).WithMessage("This field must be greater than 0.").NotEmpty();

            RuleFor(exercise => exercise.IdType).GreaterThan(0).NotEmpty().WithMessage("This field is required and it must be greater than 0.");

            RuleFor(exercise => exercise.IdSegment).GreaterThan(0).WithMessage("This field must be greater than 0.").NotEmpty();

        }
    }
}
