using FluentValidation;
using Models;

namespace API.Validators
{
    public class SegmentValidator : AbstractValidator<Segment>
    {
        ILogger<SegmentValidator> _logger;

        public SegmentValidator(ILogger<SegmentValidator> logger)
        {
            _logger = logger;

            RuleFor(segment => segment.Name).NotEmpty().WithMessage("This field is required");
            RuleFor(segment => segment.Name).MaximumLength(80).WithMessage("This field is can not has more than 80 characters");
        }
    }
}
