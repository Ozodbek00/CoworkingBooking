using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateAsync(OrderDTO orderDTO);
        Task<OrderDTO> UpdateAsync(OrderDTO orderDTO);
        Task DeleteAsync(long id);
        Task<OrderDTO> GetAsync(Expression<Func<Order, bool>> expression);
        Task<OrderDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Order, bool>> expression);
    }
}
