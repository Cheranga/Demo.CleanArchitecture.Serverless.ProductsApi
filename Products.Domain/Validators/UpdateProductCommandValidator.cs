using FluentValidation;
using Products.Domain.Commands;

namespace Products.Domain.Validators
{
    public class UpdateProductCommandValidator : ModelValidatorBase<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.CorrelationId).NotNull().NotEmpty();
            RuleFor(x => x.Code).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}