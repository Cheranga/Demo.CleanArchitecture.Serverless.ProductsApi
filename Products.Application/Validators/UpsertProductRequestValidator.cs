using FluentValidation;
using Products.Application.Requests;
using Products.Domain.Validators;

namespace Products.Application.Validators
{
    public class UpsertProductRequestValidator : ModelValidatorBase<UpsertProductRequest>
    {
        public UpsertProductRequestValidator()
        {
            RuleFor(x => x.CorrelationId).NotNull().NotEmpty();
            RuleFor(x => x.Code).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
        }
    }
}