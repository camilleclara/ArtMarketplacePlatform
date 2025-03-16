namespace backend_app.Models.DTO
{
    public class ProductDTO
    {

        public ProductDTO() { }
        public int Id { get; set; }

        public int? ArtisanId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public double? Price { get; set; }

        public string? Category { get; set; }
        public bool? IsAvailable { get; set; }

    }
}
