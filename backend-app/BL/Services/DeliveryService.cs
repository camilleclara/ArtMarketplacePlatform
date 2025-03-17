using AutoMapper;
using BL.Models;
using BL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using Domain;
namespace BL.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _repository;
        private readonly IMapper _mapper;

        public DeliveryService(IDeliveryRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<DeliveryDTO>> GetAllAsync()
        {
            var orders = await _repository.GetAll();
            var orderDTOS = _mapper.Map<IEnumerable<DeliveryDTO>>(orders);
            return orderDTOS;
        }

        public async Task<DeliveryDTO> GetByIdAsync(int id)
        {
            var order = await _repository.GetById(id);
            return _mapper.Map<DeliveryDTO>(order);
        }

        public async Task<DeliveryDTO> AddAsync(DeliveryDTO newDelivery)
        {
            var newDeliveryEntity = _mapper.Map<Delivery>(newDelivery);
            await _repository.Insert(newDeliveryEntity);
            return _mapper.Map<DeliveryDTO>(newDelivery);
        }

        public async Task<DeliveryDTO> UpdateAsync(int id, DeliveryDTO orderToUpdate)
        {
            var orderToUpdateEntity = _mapper.Map<Delivery>(orderToUpdate);
            await _repository.Update(id, orderToUpdateEntity);
            return _mapper.Map<DeliveryDTO>(orderToUpdateEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteById(id);
        }

        public async Task<DeliveryDTO> GetActiveDeliveryForOrder(int orderId)
        {
            var deliveriesForOrder = await _repository.GetByOrderId(orderId);
            //Ne garder que celle qui se sont pas supprimées
            var activeDelivery = deliveriesForOrder.Where(d => d.IsActive);
            return _mapper.Map<DeliveryDTO>(activeDelivery);
        }

        public async Task<int> GetActiveDeliveryIdForOrder(int orderId)
        {
            var del = GetActiveDeliveryForOrder(orderId);
            return del.Id;
        }


    }
}
