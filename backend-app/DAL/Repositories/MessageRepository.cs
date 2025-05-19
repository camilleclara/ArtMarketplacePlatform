using DAL.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MarketPlaceContext _context;

        public MessageRepository(MarketPlaceContext context)
        {
            _context = context;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _context.Messages
                                     .Where(p => p.Id == id)
                                     .ExecuteDeleteAsync();
            return result;
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _context.Messages
                .Include(r => r.MsgFrom)
                .Include(r => r.MsgTo)
                .Include(r => r.Product)
                .ToListAsync();
        }

        public async Task<Message> GetById(int id)
        {
            return await _context.Messages
                .Include(r => r.MsgFrom)
                .Include(r => r.MsgTo)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Message> Insert(Message entity)
        {
            await _context.Messages.AddAsync(entity);
            return entity;
        }
        public async Task<Message> Update(int id, Message entityUpdated)
        {
            var storedMessage = await _context.Messages
                .Where(p => p.Id == id)
                .Include(r => r.MsgFrom)
                .Include(r => r.MsgTo)
                .Include(r => r.Product)
                .FirstOrDefaultAsync();
            if (storedMessage == null)
            {
                throw new ArgumentException();
            }
            storedMessage.ProductId = entityUpdated.ProductId;
            storedMessage.MsgFromId = entityUpdated.MsgFromId;
            storedMessage.MsgToId = entityUpdated.MsgToId;
            storedMessage.Content = entityUpdated.Content;
            storedMessage.MsgTo = entityUpdated.MsgTo;
            _context.Messages.Update(storedMessage);
            return (storedMessage);
        }
        public async Task<IEnumerable<Message>> GetByProductId(int id)
        {
            var storedMessagesForArtisanId = await _context.Messages
                                         .Where(r => r.ProductId == id)
                                         .Include(r => r.MsgFrom)
                                         .Include(r => r.MsgTo)
                                         .ToListAsync();

            return storedMessagesForArtisanId;
        }

        public async Task<IEnumerable<Message>> GetByUserId(int id)
        {
            var storedMessagesForUserId = await _context.Messages
                                         .Include(r => r.MsgFrom)
                                         .Include(r => r.MsgTo)
                                         .Include(r => r.MsgFrom)
                                         .Where(r => r.MsgFromId == id || r.MsgToId == id)
                                         .ToListAsync();

            return storedMessagesForUserId;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        

        public void Delete(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
