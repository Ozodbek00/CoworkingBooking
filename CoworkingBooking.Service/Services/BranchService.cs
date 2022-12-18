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
    public class BranchService : IBranchService
    {
        private readonly IRepository<Branch> repository;
        private readonly IMapper mapper;

        public BranchService(IRepository<Branch> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BranchDTO> CreateAsync(BranchDTO branchDTO)
        {
            var branch = await repository.GetAsync(expression: s =>
                  s.Name.Equals(branchDTO.Name));

            if (branch is not null)
                throw new CBException(400, "Branch with this name exists");

            Branch mappedBranch = mapper.Map<Branch>(branchDTO);
            mappedBranch.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(mappedBranch);

            return branchDTO;
        }

        public async Task DeleteAsync(long id)
        {
            var Branch = await repository.GetAsync(expression: s => s.Id == id);

            if (Branch == null)
                throw new CBException(404, "Branch not found");


            await repository.DeleteAsync(Branch);
        }

        public async Task<BranchDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Branch, bool>> expression = null)
        {
            return await repository.GetAll(pageIndex, pageSize, expression)
                .ProjectTo<BranchDTO>(mapper.ConfigurationProvider).ToArrayAsync();
        }

        public async Task<BranchDTO> GetAsync(Expression<Func<Branch, bool>> expression)
        {
            var Branch = await repository.GetAsync(expression);

            if (Branch is null)
                throw new CBException(404, "Branch not found");

            return mapper.Map<BranchDTO>(Branch);
        }

        public async Task<BranchDTO> UpdateAsync(long id, BranchDTO branchDTO)
        {
            var branch = await repository.GetAsync(expression: s => s.Id == id);

            if (branch is null)
                throw new CBException(404, "Branch with this name does not exist");

            Branch mappedBranch = mapper.Map<Branch>(branchDTO);
            mappedBranch.CreatedAt = branch.CreatedAt;
            mappedBranch.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(mappedBranch);

            return branchDTO;
        }
    }
}
