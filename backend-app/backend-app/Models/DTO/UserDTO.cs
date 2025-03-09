namespace backend_app.Models.DTO
{
    public class UserDTO
    {
        public UserDTO(string login, string firstName, string lastName, string hashedPassword, string salt, string role) 
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            HashedPassword = hashedPassword;
            Salt = salt;
            UserType = role;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Login { get; set; }
        public string? HashedPassword { get; set; }

        public string? Salt { get; set; }
        public string? UserType { get; set; }

    }
}
