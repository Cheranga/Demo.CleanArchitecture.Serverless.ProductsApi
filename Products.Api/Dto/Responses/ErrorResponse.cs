using System.Collections.Generic;

namespace Products.Api.Dto.Responses
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public List<ErrorData> Errors { get; set; } = new List<ErrorData>();
    }
}