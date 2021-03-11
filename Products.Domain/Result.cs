using FluentValidation.Results;

namespace Products.Domain
{
    public class Result<T>
    {
        public T Data { get; set; }

        public string ErrorMessage { get; set; }

        public bool Status => !string.IsNullOrWhiteSpace(ErrorMessage);

        //public ValidationResult Validation { get; set; } = new ValidationResult();
        //public bool Status => Validation != null && Validation.IsValid;

        //public static Result<T> Success(T data)
        //{
        //    return new Result<T>
        //    {
        //        Data = data
        //    };
        //}

        //public static Result<T> Failure(params ValidationFailure[] failures)
        //{
        //    return new Result<T>
        //    {
        //        Validation = new ValidationResult(failures)
        //    };
        //}

        //public static Result<T> Failure(ValidationResult validationResult)
        //{
        //    return new Result<T>
        //    {
        //        Validation = validationResult
        //    };
        //}

        //public static Result<T> Failure(string field, string failure)
        //{
        //    return Failure(new ValidationFailure(field, failure));
        //}

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>
            {
                ErrorMessage = errorMessage
            };
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>
            {
                Data = data,
                ErrorMessage = null
            };
        }
    }
}