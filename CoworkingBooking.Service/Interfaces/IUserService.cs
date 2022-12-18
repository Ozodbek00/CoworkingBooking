using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(UserDTO userDTO);
        Task<UserDTO> UpdateAsync(UserDTO userDTO);
        Task DeleteAsync(long id);
        Task<UserDTO> GetAsync(Expression<Func<User, bool>> expression);
        Task<UserDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<User, bool>> expression);
    }
}
