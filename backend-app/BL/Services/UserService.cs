using AutoMapper;
using BL.Models;
using BL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using Domain;

namespace BL.Services
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

        public async Task<UserSafeDTO> GetByIdAsync(int id)
        {
            var user = await _repository.GetById(id);
            return _mapper.Map<UserSafeDTO>(user);
        }

        public async Task<UserDTO> AddAsync(UserDTO newUser)
        {
            var newUserEntity = _mapper.Map<User>(newUser);
            await _repository.Insert(newUserEntity);
            return _mapper.Map<UserDTO>(newUser);
        }

        public async Task<UserSafeDTO> UpdateAsync(int id, UserSafeDTO userToUpdate)
        {
            var userToUpdateEntity = _mapper.Map<User>(userToUpdate);
            await _repository.Update(id, userToUpdateEntity);
            return _mapper.Map<UserSafeDTO>(userToUpdateEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteById(id);
        }

        Task<UserDTO> IGenericService<UserDTO>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> UpdateAsync(int id, UserDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
