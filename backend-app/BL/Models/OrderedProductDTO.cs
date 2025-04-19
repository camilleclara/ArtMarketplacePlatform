using Domain;

namespace BL.Models
{
    public class OrderedProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double? Price { get; set; }
        public int Quantity { get; set; }
    }
}
