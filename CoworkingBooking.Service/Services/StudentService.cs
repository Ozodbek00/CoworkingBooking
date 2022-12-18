using AutoMapper;
using CoworkingBooking.Data.Interfaces;
using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using CoworkingBooking.Service.Interfaces;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> repository;
        private readonly IMapper mapper;

        public StudentService(IRepository<Student> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<StudentDTO> CreateAsync(StudentDTO studentDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO[]> GetAllAsync(Expression<Func<Student, bool>> expression, int? pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO> GetAsync(Expression<Func<Student, bool>> expression, StudentDTO studentDto)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO> UpdateAsync(StudentDTO studentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
