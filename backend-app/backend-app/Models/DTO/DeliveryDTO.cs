using backend_app.Models.Enums;

namespace backend_app.Models.DTO
{
    public class DeliveryDTO
    {

        public DeliveryDTO() { }
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public Boolean? IsActive { get; set; }

        public string? DeliStatus { get; set; }

        public DateOnly? EstimatedDate { get; set; }

        public DateTime? DeliveryDate { get; set; }


    }
}
