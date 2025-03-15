using backend_app.Models.Enums;

namespace backend_app.Models.DTO
{
    public class OrderDTO
    {

        public OrderDTO() { }

        public int Id { get; set; }
        public int? ArtisanId { get; set; }
        public int? CustomerId { get; set; }
        public DeliveryDTO ActiveDelivery { get; set; }  
      
    }
}
