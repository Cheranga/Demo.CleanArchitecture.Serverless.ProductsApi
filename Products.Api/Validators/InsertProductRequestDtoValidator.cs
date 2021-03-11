using FluentValidation;
using Products.Api.Dto.Requests;

namespace Products.Api.Validators
{
    public class InsertProductRequestDtoValidator : ModelValidatorBase<InsertProductRequestDto>
    {
        public InsertProductRequestDtoValidator()
        {
            RuleFor(x => x.ProductCode).NotNull().NotEmpty();
            RuleFor(x => x.ProductDescription).NotNull().NotEmpty();
        }
    }
}