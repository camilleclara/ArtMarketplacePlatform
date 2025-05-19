using Domain;

namespace BL.Models
{
    public class ProductDTO
    {

        public ProductDTO() { }
        public int Id { get; set; }

        public int? ArtisanId { get; set; }
        public string? ArtisanName { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public double? Price { get; set; }

        public string? Category { get; set; }
        public bool? IsAvailable { get; set; }
        public virtual ICollection<ProductImageDTO> ProductImages { get; set; } = new List<ProductImageDTO>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public bool? IsActive { get; internal set; }
    }
}
