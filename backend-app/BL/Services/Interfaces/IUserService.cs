using BL.Models;

namespace BL.Services.Interfaces

{
    public interface IUserService: IGenericService<UserDTO>
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int id);
        Task<UserDTO> AddAsync(UserDTO entity);
        Task<UserDTO> UpdateAsync(int id, UserDTO entity);
        Task<int> DeleteAsync(int id);
    }
}
