﻿namespace BL.Models
{
    public class UserDTO
    {
        public UserDTO(string login, string firstName, string lastName, string hashedPassword, string salt, string role, string fullAddress)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            HashedPassword = hashedPassword;
            Salt = salt;
            UserType = role;
            Address = fullAddress;  
        }
        public UserDTO() { }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string Login { get; set; }
        public string? HashedPassword { get; set; }

        public string? Salt { get; set; }
        public string? UserType { get; set; }


    }
}
