using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Interfaces
{
    public interface ITableService
    {
        Task<TableDTO> CreateAsync(TableDTO tableDTO);
        Task<TableDTO> UpdateAsync(TableDTO tableDTO);
        Task DeleteAsync(long id);
        Task<TableDTO> GetAsync(Expression<Func<Table, bool>> expression);
        Task<TableDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Table, bool>> expression);
    }
}
