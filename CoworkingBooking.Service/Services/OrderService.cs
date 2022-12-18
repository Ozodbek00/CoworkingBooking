using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoworkingBooking.Data.Interfaces;
using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using CoworkingBooking.Service.Exceptions;
using CoworkingBooking.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> repository;
        private readonly IRepository<Chair> chairRepo;
        private readonly IRepository<User> userRepo;
        private readonly IMapper mapper;

        public OrderService(IRepository<Order> repository,
                            IRepository<Chair> chairRepo,
                            IRepository<User> userRepo,
                            IMapper mapper)
        {
            this.repository = repository;
            this.chairRepo = chairRepo;
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        public async Task<OrderDTO> CreateAsync(OrderDTO orderDTO)
        {
            var order = repository.GetAsync(expression: s => s.StartAt == orderDTO.StartAt);

            if (order is not null)
                throw new CBException(400, "Order with this index exists");

            var chair = chairRepo.GetAsync(b => b.Id == orderDTO.ChairId);

            if (chair is null)
                throw new CBException(404, "Chair with this id noes not exist");

            var user = userRepo.GetAsync(b => b.Id == orderDTO.UserId);

            if (user is null)
                throw new CBException(404, "User with this id noes not exist");

            Order mappedOrder = mapper.Map<Order>(orderDTO);
            mappedOrder.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(mappedOrder);

            return orderDTO;
        }

        public async Task DeleteAsync(long id)
        {
            var Order = await repository.GetAsync(expression: s => s.Id == id);

            if (Order == null)
                throw new CBException(404, "Order not found");


            await repository.DeleteAsync(Order);
        }

        public async Task<OrderDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Order, bool>> expression)
        {
            return await repository.GetAll(pageIndex, pageSize, expression)
                .ProjectTo<OrderDTO>(mapper.ConfigurationProvider).ToArrayAsync();
        }

        public async Task<OrderDTO> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var Order = await repository.GetAsync(expression);

            if (Order is null)
                throw new CBException(404, "Order not found");

            return mapper.Map<OrderDTO>(Order);
        }

        public async Task<OrderDTO> UpdateAsync(OrderDTO orderDTO)
        {
            var order = repository.GetAsync(expression: s => s.StartAt == orderDTO.StartAt);

            if (order is null)
                throw new CBException(404, "Order with this index does not exist");

            var chair = chairRepo.GetAsync(b => b.Id == orderDTO.ChairId);

            if (chair is null)
                throw new CBException(404, "Chair with this id noes not exist");

            var user = userRepo.GetAsync(b => b.Id == orderDTO.ChairId);

            if (user is null)
                throw new CBException(404, "User with this id noes not exist");

            Order mappedOrder = mapper.Map<Order>(orderDTO);
            mappedOrder.CreatedAt = order.Result.CreatedAt;
            mappedOrder.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(mappedOrder);

            return orderDTO;
        }
    }
}
