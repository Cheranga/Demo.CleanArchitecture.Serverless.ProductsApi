using FluentValidation;
using Products.Api.Dto.Requests;
using Products.Domain.Validators;

namespace Products.Api.Validators
{
    public class UpsertProductRequestDtoValidator : ModelValidatorBase<UpsertProductRequestDto>
    {
        public UpsertProductRequestDtoValidator()
        {
            RuleFor(x => x.CorrelationId).NotNull().NotEmpty();
            RuleFor(x => x.ProductCode).NotNull().NotEmpty();
            RuleFor(x => x.ProductDescription).NotNull().NotEmpty();
        }
    }
}