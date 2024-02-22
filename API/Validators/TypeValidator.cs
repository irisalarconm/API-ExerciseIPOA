using FluentValidation;

namespace API.Validators
{
    public class TypeValidator : AbstractValidator<Models.Type>
    {
        ILogger<TypeValidator> _logger;

        public TypeValidator(ILogger<TypeValidator> logger)
        {
            _logger = logger;

            RuleFor(type => type.Name).NotEmpty().WithMessage("This field is required");
            RuleFor(type => type.Name).MaximumLength(80).WithMessage("This field is can not has more than 80 characters");
        }
    }
}
