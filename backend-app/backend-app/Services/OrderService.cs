using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;
using backend_app.Repositories;
using backend_app.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace backend_app.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllAsync()
        {
            var orders = await _repository.GetAll();
            var orderDTOS = _mapper.Map<IEnumerable<OrderDTO>>(orders);
            return orderDTOS;
        }

        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            var order = await _repository.GetById(id);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> AddAsync(OrderDTO newOrder)
        {
            var newOrderEntity = _mapper.Map<Order>(newOrder);
            await _repository.Insert(newOrderEntity);
            return _mapper.Map<OrderDTO>(newOrder);
        }

        public async Task<OrderDTO> UpdateAsync(int id, OrderDTO orderToUpdate)
        {
            var orderToUpdateEntity = _mapper.Map<Order>(orderToUpdate);
            await _repository.Update(id, orderToUpdateEntity);
            return _mapper.Map<OrderDTO>(orderToUpdateEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteById(id);
        }

        public async Task<IEnumerable<OrderDTO>> GetByCustomerId(int customerId)
        {
            var orders = await _repository.GetByCustomerId(customerId);
            var deliveries = orders.FirstOrDefault().Deliveries;
            var orderDTOS = _mapper.Map<IEnumerable<OrderDTO>>(orders);
            return orderDTOS;
        }

    }
}
