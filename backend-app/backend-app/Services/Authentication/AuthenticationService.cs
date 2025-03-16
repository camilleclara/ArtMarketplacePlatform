using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace backend_app.Services.Authentication
{
    public class AuthenticationService
    {
        IConfiguration _config;

        private readonly MarketPlaceContext _context;
        private readonly IMapper _mapper;
        public AuthenticationService(IConfiguration config, MarketPlaceContext context, IMapper mapper)
        {
            _config = config;
            _context = context;
            _mapper = mapper;

        }
        private string GenerateJSONWebToken(string username)
        {
            var secretKey = _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

  
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("custom_info", "info"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, _context.Users.FirstOrDefault(x=>x.Login==username).UserType)
            };

            var jwtIssuer = _config["Jwt:Issuer"];
            var jwtAudience = _config["Jwt:Audience"];

            var token = new JwtSecurityToken(
                jwtIssuer,
                jwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password, string salt)
        {

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                    password: Encoding.UTF8.GetBytes(password),
                    salt: Encoding.UTF8.GetBytes(salt),
                    iterations: 10,
                    hashAlgorithm: HashAlgorithmName.SHA512,
                    outputLength: 10);

            return Convert.ToHexString(hash);
        }

        public async Task<UserDTO> RegisterUser(string login, string firstName, string lastName,string password, string role)
        {
            //if (users.Any(user => user.Username.ToLower() == username.ToLower()))
            //{
            //    throw new Exception("User already exist");
            //}
            var salt = DateTime.Now.ToString("dddd"); // get the day of week. Ex: Sunday
            var passwordHash = HashPassword(password, salt);
            var newUser = new UserDTO(login,firstName,lastName, passwordHash, salt, role);
            await _context.Users.AddAsync(_mapper.Map<User>(newUser));
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(newUser);

        }

        public string Login(string username, string password)
        {

            var user = _context.Users.FirstOrDefault(user => user.Login.ToLower() == username.ToLower()) ??
                            throw new Exception("Login failed; Invalid userID or password");

            var passwordHash = HashPassword(password, user.Salt);
            if (user.HashedPassword == passwordHash)
            {
                var token = GenerateJSONWebToken(username);
                return token;
            }
            throw new Exception("Login failed; Invalid userID or password");
        }

    }

    //public class User
    //{
    //    public string Username { get; set; }

    //    public User(string username, string password, string salt, string role)
    //    {
    //        Username = username;
    //        Password = password;
    //        Salt = salt;
    //        Role = role;
    //    }

    //    public User(string username, string password)
    //    {
    //        Username = username;
    //        Password = password;
    //        Role = Role;
    //    }

    //    public string Password { get; set; }
    //    public string Salt { get; set; }
    //    public string Role { get; set; }
    //}
}