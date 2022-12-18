using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Interfaces
{
    public interface IBranchService
    {
        Task<BranchDTO> CreateAsync(BranchDTO branchDTO);
        Task<BranchDTO> UpdateAsync(long id, BranchDTO branchDTO);
        Task DeleteAsync(long id);
        Task<BranchDTO> GetAsync(Expression<Func<Branch, bool>> expression);
        Task<BranchDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Branch, bool>> expression = null);
    }
}
