using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;
using backend_app.Repositories;

namespace backend_app.Services.Interfaces
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
