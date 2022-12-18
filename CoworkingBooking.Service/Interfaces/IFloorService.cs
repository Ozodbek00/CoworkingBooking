using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Interfaces
{
    public interface IFloorService
    {
        Task<FloorDTO> CreateAsync(FloorDTO floorDTO);
        Task<FloorDTO> UpdateAsync(FloorDTO floorDTO);
        Task DeleteAsync(long id);
        Task<FloorDTO> GetAsync(Expression<Func<Floor, bool>> expression);
        Task<FloorDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Floor, bool>> expression);
    }
}
