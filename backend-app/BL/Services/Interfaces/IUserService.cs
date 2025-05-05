using BL.Models;

namespace BL.Services.Interfaces

{
    public interface IUserService: IGenericService<UserDTO>
    {
        Task<IEnumerable<UserSafeDTO>> GetAllAsync();
        Task<UserSafeDTO> GetByIdAsync(int id);
        Task<UserSafeDTO> ApproveAsync(int id);
        Task<UserSafeDTO> DeactivateAsync(int id);
        Task<UserDTO> AddAsync(UserDTO entity);
        Task<UserSafeDTO> UpdateAsync(int id, UserSafeDTO entity, bool adminRights = false);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<UserSafeDTO>> GetAllDeliveryPartnerUsersAsync();
        Task<IEnumerable<UserSafeDTO>> GetAllArtisansUsersAsync();
    }
}
