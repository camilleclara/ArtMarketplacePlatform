using Domain;

namespace BL.Models
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? CustomerId { get; set; }

        public string? Content { get; set; }

        public bool? FromArtisan { get; set; }

        public bool IsActive { get; set; }

        public int? Score { get; set; }

        public virtual UserSafeDTO? Customer { get; set; }

        public virtual ProductDTO? Product { get; set; }
        public DateTime? Created { get; set; }
    }
}
