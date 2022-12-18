using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoworkingBooking.Data.Interfaces;
using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;
using CoworkingBooking.Service.Exceptions;
using CoworkingBooking.Service.Extensions;
using CoworkingBooking.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoworkingBooking.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public UserService(IRepository<User> repository, IMapper mapper, IAuthService service)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.authService= service;
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDTO)
        {
            var user = await repository.GetAsync(expression: s =>
                  s.PhoneNumber.Equals(userDTO.PhoneNumber));

            if (user is not null)
                throw new CBException(400, "User with this phone number exists");

            var validUser = await repository.GetAsync(expression: s =>
                  s.Username.Equals(userDTO.Username) ||
                  s.Password.Equals(userDTO.Password.Encrypt()));

            if (validUser is not null)
                throw new CBException(400, "Invalid password or username");

            User mappedUser = mapper.Map<User>(userDTO);
            mappedUser.CreatedAt = DateTime.UtcNow;
            mappedUser.Password = mappedUser.Password.Encrypt();

            await repository.AddAsync(mappedUser);

            return userDTO;
        }

        public async Task DeleteAsync(long id)
        {
            var user = await repository.GetAsync(expression: s => s.Id == id);

            if (user == null)
                throw new CBException(404, "User not found");


            await repository.DeleteAsync(user);
        }

        public async Task<UserDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<User, bool>> expression = null)
        {
            return await repository.GetAll(pageIndex, pageSize, expression)
                .ProjectTo<UserDTO>(mapper.ConfigurationProvider).ToArrayAsync();
        }

        public async Task<UserDTO> GetAsync(Expression<Func<User, bool>> expression)
        {
            var User = await repository.GetAsync(expression);

            if (User is null)
                throw new CBException(404, "User not found");

            return mapper.Map<UserDTO>(User);
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await repository.GetAsync(x => x.Username == username && x.Password == password.Encrypt());
            if (user is null)
                throw new CBException(404, "User not found!");

            return await authService.GenerateTokenAsync(user);
        }

        public async Task<UserDTO> UpdateAsync(long id, UserDTO userDTO)
        {
            var user = await repository.GetAsync(expression: s => s.Id == id);

            if (user is null)
                throw new CBException(404, "User not found!");

            var passUser = await repository.GetAsync(u => u.Password.Equals(userDTO.Password.Encrypt()));

            User mappedUser = mapper.Map<User>(userDTO);
            mappedUser.CreatedAt = user.CreatedAt;
            mappedUser.UpdatedAt = DateTime.UtcNow;
            mappedUser.Password = mappedUser.Password.Encrypt();

            await repository.UpdateAsync(mappedUser);

            return userDTO;
        }
    }
}