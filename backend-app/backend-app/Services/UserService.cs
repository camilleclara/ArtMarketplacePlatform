using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;
using backend_app.Repositories.Interfaces;
using backend_app.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _repository.GetAll();
            var userDTOS = _mapper.Map<IEnumerable<UserDTO>>(users);
            return userDTOS;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _repository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> AddAsync(UserDTO newUser)
        {
            var newUserEntity = _mapper.Map<User>(newUser);
            await _repository.Insert(newUserEntity);
            return _mapper.Map<UserDTO>(newUser);
        }

        public async Task<UserDTO> UpdateAsync(int id, UserDTO userToUpdate)
        {
            var userToUpdateEntity = _mapper.Map<User>(userToUpdate);
            await _repository.Update(id, userToUpdateEntity);
            return _mapper.Map<UserDTO>(userToUpdateEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteById(id);
        }
    }
}
