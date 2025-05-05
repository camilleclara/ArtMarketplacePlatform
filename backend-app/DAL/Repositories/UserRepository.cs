using DAL.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
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
            return await _context.Users
                .Include(u => u.OrderArtisans)
                .Include(u => u.OrderCustomers).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllDeliveryPartners()
        {
            return await _context.Users.Where(t => t.UserType == "DELIVERYPARTNER").ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            var result = await _context.Users
                .Include(u => u.OrderArtisans)
                .Include(u => u.OrderCustomers)
                .FirstOrDefaultAsync(u => u.Id == id);

            return result;
        }

        public async Task<User> Insert(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> Update(int id, User entityUpdated, bool adminRights = false)
        {
            var storedUser = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (storedUser == null)
            {
                throw new ArgumentException();//TODO throw error not found
            }
            storedUser.FirstName = entityUpdated.FirstName;
            storedUser.LastName = entityUpdated.LastName;
            if (adminRights)
            {
                storedUser.Login = entityUpdated.Login;
                storedUser.IsActive = entityUpdated.IsActive;
                storedUser.UserType = entityUpdated.UserType;
            }
            _context.Users.Update(storedUser);
            await _context.SaveChangesAsync();
            return (storedUser);
        }

        public async Task<User> ApproveAsync(int id)
        {
            var storedUser = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (storedUser == null)
            {
                throw new ArgumentException();//TODO throw error not found
            }
            storedUser.IsActive = true;
            await _context.SaveChangesAsync();
            return (storedUser);
        }

        public async Task<User> DeactivateAsync(int id)
        {
            var storedUser = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (storedUser == null)
            {
                throw new ArgumentException();//TODO throw error not found
            }
            storedUser.IsActive = false;
            await _context.SaveChangesAsync();
            return (storedUser);
        }

        public async Task<User> Update(int id, User entity)
        {
            return await this.Update(id, entity, false);
        }

    }
}
