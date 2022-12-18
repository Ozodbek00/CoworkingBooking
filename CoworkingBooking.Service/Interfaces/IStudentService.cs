using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDTO> CreateAsync(StudentDTO studentDTO);
        Task<StudentDTO> UpdateAsync(StudentDTO studentDTO);
        Task DeleteAsync(long id);
        Task<StudentDTO> GetAsync(Expression<Func<Student, bool>> expression, StudentDTO studentDto);
        Task<StudentDTO[]> GetAllAsync(Expression<Func<Student, bool>> expression, int? pageIndex, int pageSize);
    }
}
