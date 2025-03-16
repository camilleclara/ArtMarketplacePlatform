namespace backend_app.Models.DTO
{
    public class ProductImageDTO
    {

        public ProductImageDTO() { }
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public string? Content { get; set; } 

        public string Name { get; set; }
        public string MimeType { get; set; }

    }
}
