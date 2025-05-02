namespace BL.Models
{
    public class UserSafeDTO
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string? UserType { get; set; }
        public int? OrdersPlaced { get; set; }
        public int? OrdersFulfilled { get; set; }
    }
}
