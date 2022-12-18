using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginAsync(string username, string password);
        Task<UserDTO> CreateAsync(UserDTO userDTO);
        Task<UserDTO> UpdateAsync(long id, UserDTO userDTO);
        Task DeleteAsync(long id);
        Task<UserDTO> GetAsync(Expression<Func<User, bool>> expression);
        Task<UserDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<User, bool>> expression = null);
    }
}
