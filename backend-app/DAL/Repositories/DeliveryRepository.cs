using DAL.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly MarketPlaceContext _context;

        public DeliveryRepository(MarketPlaceContext context)
        {
            _context = context;
        }
        public void Delete(Delivery entity)
        {
            //TODO cancel delivery
            throw new NotImplementedException();
        }

        public async Task<Delivery> GetById(int id)
        {
            return await _context.Deliveries.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _context.Deliveries
                                     .Where(t => t.Id == id)
                                     .ExecuteDeleteAsync();
            return result;
        }

        public async Task<IEnumerable<Delivery>> GetAll()
        {
            return await _context.Deliveries.ToListAsync();
        }

        public async Task<Delivery> Insert(Delivery entity)
        {
            await _context.Deliveries.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Delivery> Update(int id, Delivery entityUpdated)
        {
            var storedDelivery = await _context.Deliveries.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (storedDelivery == null)
            {
                throw new ArgumentException();
            }
            storedDelivery.OrderId = entityUpdated.OrderId;
            storedDelivery.DeliStatus = entityUpdated.DeliStatus;
            storedDelivery.EstimatedDate = entityUpdated.EstimatedDate;
            _context.Deliveries.Update(storedDelivery);
            await _context.SaveChangesAsync();
            return (storedDelivery);
        }

        public async Task<Delivery> UpdateStatusById(int id, string newStatus)
        {
            var storedDelivery = await _context.Deliveries.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (storedDelivery == null)
            {
                throw new ArgumentException();
            }
            storedDelivery.DeliStatus = newStatus;
            _context.Deliveries.Update(storedDelivery);
            await _context.SaveChangesAsync();
            return (storedDelivery);
        }

        public async Task<IEnumerable<Delivery>> GetByOrderId(int id)
        {
            var storedDeliveriesForOrder = await _context.Deliveries
                             .Where(d => d.Order.Id == id)
                             .ToListAsync();
            return storedDeliveriesForOrder;
        }
    }
}
