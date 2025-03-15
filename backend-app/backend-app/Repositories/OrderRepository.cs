using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Repositories
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

        public async Task<IEnumerable<Order>> GetByArtisantId(int artisanId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetByArtisanId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateStatusById(int id, DeliveryStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
