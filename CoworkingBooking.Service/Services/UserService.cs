﻿using AutoMapper;
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
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;
        private readonly IMapper mapper;

        public UserService(IRepository<User> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDTO)
        {
            var user = repository.GetAsync(expression: s =>
                  s.PhoneNumber.Equals(userDTO.PhoneNumber, StringComparison.CurrentCultureIgnoreCase));

            if(user is not null)
                throw new CBException(400, "User with this phone number exists");

            User mappedUser = mapper.Map<User>(userDTO);
            mappedUser.CreatedAt = DateTime.UtcNow;

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

        public async Task<UserDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<User, bool>> expression)
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

        public async Task<UserDTO> UpdateAsync(UserDTO userDTO)
        {
            var User = repository.GetAsync(expression: s =>
                  s.PhoneNumber.Equals(userDTO.PhoneNumber, StringComparison.CurrentCultureIgnoreCase));

            if (User is null)
                throw new CBException(404, "User with this phone number does not exist");

            User mappedUser = mapper.Map<User>(userDTO);
            mappedUser.CreatedAt = User.Result.CreatedAt;
            mappedUser.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(mappedUser);

            return userDTO;
        }
    }
}