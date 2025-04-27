using AutoMapper;
using BL.Models;
using BL.Models.Enums;
using BL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using Domain;
namespace BL.Services
{
    public class MessageService : IMessageService
    {

        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;


        public MessageService(IMessageRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<MessageDTO>> GetAllAsync()
        {
            var messages = await _repository.GetAll();
            var messageDTOS = _mapper.Map<IEnumerable<MessageDTO>>(messages);
            return messageDTOS;
        }

        public async Task<MessageDTO> GetByIdAsync(int id)
        {
            var message = await _repository.GetById(id);
            return _mapper.Map<MessageDTO>(message);
        }

        public async Task<MessageDTO> AddAsync(MessageDTO newMessage)
        {
            var newMessageEntity = _mapper.Map<Message>(newMessage);
            await _repository.Insert(newMessageEntity);
            await _repository.SaveChanges();
            return _mapper.Map<MessageDTO>(newMessage);
        }

        public async Task<MessageDTO> UpdateAsync(int id, MessageDTO messageToUpdate)
        {
            //Mapper les properties de base
            var messageToUpdateEntity = _mapper.Map<Message>(messageToUpdate);

            await _repository.Update(id, messageToUpdateEntity);
            await _repository.SaveChanges();
            return _mapper.Map<MessageDTO>(messageToUpdateEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            try {
                await _repository.DeleteById(id);
                await _repository.SaveChanges();
                return 1;
            }
            catch (Exception ex) { return 0; }
        }


        public async Task<IEnumerable<MessageDTO>> GetByUserId(int artisanId)
        {
            var messagesEntities = await _repository.GetByUserId(artisanId);
            return _mapper.Map<IEnumerable<MessageDTO>>(messagesEntities);
        }

        public async Task<IEnumerable<MessageDTO>> GetByProductId(int artisanId)
        {
            var messagesEntities = await _repository.GetByProductId(artisanId);
            return _mapper.Map<IEnumerable<MessageDTO>>(messagesEntities);
        }

    }
}
