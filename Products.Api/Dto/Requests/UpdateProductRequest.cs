namespace Products.Api.Dto.Requests
{
    public class UpdateProductRequest
    {
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
    }
}