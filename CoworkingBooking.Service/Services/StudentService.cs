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
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> repository;
        private readonly IMapper mapper;

        public StudentService(IRepository<Student> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<StudentDTO> CreateAsync(StudentDTO studentDTO)
        {
            var student = repository.GetAsync(expression: s =>
                  s.PhoneNumber.Equals(studentDTO.PhoneNumber, StringComparison.CurrentCultureIgnoreCase));

            if(student is not null)
                throw new CBException(400, "Student with this phone number exists");

            Student mappedStudent = mapper.Map<Student>(studentDTO);
            mappedStudent.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(mappedStudent);

            return studentDTO;
        }

        public async Task DeleteAsync(long id)
        {
            var student = await repository.GetAsync(expression: s => s.Id == id);

            if (student == null)
                throw new CBException(404, "Student not found");


            await repository.DeleteAsync(student);
        }

        public async Task<StudentDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Student, bool>> expression)
        {
            return await repository.GetAll(pageIndex, pageSize, expression)
                .ProjectTo<StudentDTO>(mapper.ConfigurationProvider).ToArrayAsync();
        }

        public async Task<StudentDTO> GetAsync(Expression<Func<Student, bool>> expression)
        {
            var student = await repository.GetAsync(expression);

            if (student is null)
                throw new CBException(404, "Student not found");

            return mapper.Map<StudentDTO>(student);
        }

        public async Task<StudentDTO> UpdateAsync(StudentDTO studentDTO)
        {
            var student = repository.GetAsync(expression: s =>
                  s.PhoneNumber.Equals(studentDTO.PhoneNumber, StringComparison.CurrentCultureIgnoreCase));

            if (student is null)
                throw new CBException(400, "Student with this phone number does not exist");

            Student mappedStudent = mapper.Map<Student>(studentDTO);
            mappedStudent.CreatedAt = student.Result.CreatedAt;
            mappedStudent.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(mappedStudent);

            return studentDTO;
        }
    }
}
