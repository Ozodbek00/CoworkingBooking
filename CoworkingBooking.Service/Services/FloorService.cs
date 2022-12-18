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
    public class FloorService : IFloorService
    {
        private readonly IRepository<Floor> repository;
        private readonly IRepository<Branch> branchRepo;
        private readonly IMapper mapper;

        public FloorService(IRepository<Floor> repository, IRepository<Branch> branchRepo, IMapper mapper)
        {
            this.repository = repository;
            this.branchRepo = branchRepo;
            this.mapper = mapper;
        }

        public async Task<FloorDTO> CreateAsync(FloorDTO floorDTO)
        {
            var floor = repository.GetAsync(expression: s => s.Index == floorDTO.Index);

            if (floor is not null)
                throw new CBException(400, "Floor with this index exists");

            var branch = branchRepo.GetAsync(b => b.Id == floorDTO.BranchId);

            if (branch is null)
                throw new CBException(404, "Branch with this id noes not exist");

            Floor mappedFloor = mapper.Map<Floor>(floorDTO);
            mappedFloor.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(mappedFloor);

            return floorDTO;
        }

        public async Task DeleteAsync(long id)
        {
            var Floor = await repository.GetAsync(expression: s => s.Id == id);

            if (Floor == null)
                throw new CBException(404, "Floor not found");


            await repository.DeleteAsync(Floor);
        }

        public async Task<FloorDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Floor, bool>> expression)
        {
            return await repository.GetAll(pageIndex, pageSize, expression)
                .ProjectTo<FloorDTO>(mapper.ConfigurationProvider).ToArrayAsync();
        }

        public async Task<FloorDTO> GetAsync(Expression<Func<Floor, bool>> expression)
        {
            var Floor = await repository.GetAsync(expression);

            if (Floor is null)
                throw new CBException(404, "Floor not found");



            return mapper.Map<FloorDTO>(Floor);
        }

        public async Task<FloorDTO> UpdateAsync(FloorDTO floorDTO)
        {
            var Floor = repository.GetAsync(expression: s => s.Index == floorDTO.Index);

            if (Floor is null)
                throw new CBException(404, "Floor with this index does not exist");

            Floor mappedFloor = mapper.Map<Floor>(floorDTO);
            mappedFloor.CreatedAt = Floor.Result.CreatedAt;
            mappedFloor.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(mappedFloor);

            return floorDTO;
        }
    }
}
