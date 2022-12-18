using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Interfaces
{
    public interface IChairService
    {
        Task<ChairDTO> CreateAsync(ChairDTO chairDTO);
        Task<ChairDTO> UpdateAsync(long id, ChairDTO chairDTO);
        Task DeleteAsync(long id);
        Task<ChairDTO> GetAsync(Expression<Func<Chair, bool>> expression);
        Task<ChairDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Chair, bool>> expression = null);
    }
}
