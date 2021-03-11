using FluentValidation;
using Products.Domain.Commands;

namespace Products.Domain.Validators
{
    public class CreateProductCommandValidator : ModelValidatorBase<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.CorrelationId).NotNull().NotEmpty();
            RuleFor(x => x.Code).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}