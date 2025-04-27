using Domain;

namespace BL.Models
{
    public class  MessageDTO
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? MsgFromId { get; set; }
        public int? MsgToId { get; set; }

        public string? Content { get; set; }


        public virtual UserSafeDTO? MsgFrom { get; set; }
        public virtual UserSafeDTO? MsgTo { get; set; }

        public virtual ProductDTO? Product { get; set; }
        public DateTime? Created { get; set; }
    }
}
