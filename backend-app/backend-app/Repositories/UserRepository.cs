using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MarketPlaceContext _context;

        public UserRepository(MarketPlaceContext context)
        {
            _context = context;
        }
        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _context.Users
                                     .Where(t => t.Id == id)
                                     .ExecuteDeleteAsync();
            return result;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> Insert(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> Update(int id, User entityUpdated)
        {
            var storedUser = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (storedUser == null)
            {
                throw new ArgumentException();//TODO throw error not found
            }
            storedUser.FirstName = entityUpdated.FirstName;
            storedUser.LastName = entityUpdated.LastName;
            storedUser.Login = entityUpdated.Login;
            storedUser.Salt = entityUpdated.Salt;
            storedUser.HashedPassword = entityUpdated.HashedPassword;
            storedUser.IsActive = entityUpdated.IsActive;
            storedUser.UserType = entityUpdated.UserType;
            //TODO; update chats and other collections
            _context.Users.Update(storedUser);
            await _context.SaveChangesAsync();
            return (storedUser);
        }
    }
}
