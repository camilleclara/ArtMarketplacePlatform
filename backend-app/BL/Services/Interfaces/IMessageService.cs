using BL.Models;
using BL.Models.Enums;

namespace BL.Services.Interfaces
{ 
    public interface IMessageService: IGenericService<MessageDTO>
    {
        
        Task<IEnumerable<MessageDTO>> GetAllAsync();
        Task<MessageDTO> GetByIdAsync(int id);
        Task<MessageDTO> AddAsync(MessageDTO entity);
        Task<MessageDTO> UpdateAsync(int id, MessageDTO entity);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<MessageDTO>> GetByUserId(int userId);
        Task<IEnumerable<MessageDTO>> GetByProductId(int productId);

    }
}
