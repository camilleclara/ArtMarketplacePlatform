using BL.Models;

namespace BL.Services.Interfaces

{
    public interface IUserService: IGenericService<UserDTO>
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserSafeDTO> GetByIdAsync(int id);
        Task<UserDTO> AddAsync(UserDTO entity);
        Task<UserSafeDTO> UpdateAsync(int id, UserSafeDTO entity);
        Task<int> DeleteAsync(int id);
    }
}
