using FluentValidation;
using Products.Domain.Queries;

namespace Products.Domain.Validators
{
    public class GetProductByCodeQueryValidator : ModelValidatorBase<GetProductByCodeQuery>
    {
        public GetProductByCodeQueryValidator()
        {
            RuleFor(x => x.CorrelationId).NotNull().NotEmpty();
            RuleFor(x => x.Code).NotNull().NotEmpty();
        }
    }
}