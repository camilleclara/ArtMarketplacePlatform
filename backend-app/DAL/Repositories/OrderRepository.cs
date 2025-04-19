using DAL.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MarketPlaceContext _context;

        public OrderRepository(MarketPlaceContext context)
        {
            _context = context;
        }
        public void Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.Include(o => o.Deliveries).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _context.Orders
                                     .Where(t => t.Id == id).Include(o => o.Deliveries)
                                     .ExecuteDeleteAsync();
            return result;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> Insert(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Order> Update(int id, Order entityUpdated)
        {
            var storedOrder = await _context.Orders.Where(u => u.Id == id).Include(o => o.Deliveries).FirstOrDefaultAsync();
            if (storedOrder == null)
            {
                throw new ArgumentException();
            }
            storedOrder.CustomerId = entityUpdated.CustomerId;
            storedOrder.ArtisanId = entityUpdated.ArtisanId;
            _context.Orders.Update(storedOrder);
            await _context.SaveChangesAsync();
            return (storedOrder);
        }

        public async Task<IEnumerable<Order>> GetByCustomerId(int customerId)
        {
            var storedOrders = await _context.Orders.Where(o => o.CustomerId == customerId).Include(o => o.Deliveries).ToListAsync();
            return storedOrders;
        }

        public async Task<IEnumerable<Order>> GetByStatus(int attributeId)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Order>> GetByArtisanId(int id)
        {
            List<Order> storedOrders = await _context.Orders
                .Where(o => o.ArtisanId == id)
                .Include(o => o.Deliveries)
                .Include( o => o.ItemOrders).ThenInclude(io => io.Product)
                .ToListAsync();
            return storedOrders;
        }

        public async Task<Order> UpdateStatusById(int id, string newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
