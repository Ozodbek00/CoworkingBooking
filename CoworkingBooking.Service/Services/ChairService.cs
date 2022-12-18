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
    public class ChairService : IChairService
    {
        private readonly IRepository<Chair> repository;
        private readonly IRepository<Table> tableRepo;
        private readonly IMapper mapper;

        public ChairService(IRepository<Chair> repository, IRepository<Table> tableRepo, IMapper mapper)
        {
            this.repository = repository;
            this.tableRepo = tableRepo;
            this.mapper = mapper;
        }

        public async Task<ChairDTO> CreateAsync(ChairDTO chairDTO)
        {
            var Chair = repository.GetAsync(expression: s => s.Index == chairDTO.Index);

            if (Chair is not null)
                throw new CBException(400, "Chair with this index exists");

            var branch = tableRepo.GetAsync(b => b.Id == chairDTO.TableId);

            if (branch is null)
                throw new CBException(404, "Table with this id noes not exist");

            Chair mappedChair = mapper.Map<Chair>(chairDTO);
            mappedChair.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(mappedChair);

            return chairDTO;
        }

        public async Task DeleteAsync(long id)
        {
            var Chair = await repository.GetAsync(expression: s => s.Id == id);

            if (Chair == null)
                throw new CBException(404, "Chair not found");


            await repository.DeleteAsync(Chair);
        }

        public async Task<ChairDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Chair, bool>> expression = null)
        {
            return await repository.GetAll(pageIndex, pageSize, expression)
                .ProjectTo<ChairDTO>(mapper.ConfigurationProvider).ToArrayAsync();
        }

        public async Task<ChairDTO> GetAsync(Expression<Func<Chair, bool>> expression)
        {
            var Chair = await repository.GetAsync(expression);

            if (Chair is null)
                throw new CBException(404, "Chair not found");

            return mapper.Map<ChairDTO>(Chair);
        }

        public async Task<ChairDTO> UpdateAsync(long id, ChairDTO chairDTO)
        {
            var Chair = await repository.GetAsync(expression: s => s.Id == id);

            if (Chair is null)
                throw new CBException(404, "Chair with this index does not exist");

            var branch = tableRepo.GetAsync(b => b.Id == chairDTO.TableId);

            if (branch is null)
                throw new CBException(404, "Table with this id noes not exist");

            Chair mappedChair = mapper.Map<Chair>(chairDTO);
            mappedChair.CreatedAt = Chair.CreatedAt;
            mappedChair.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(mappedChair);

            return chairDTO;
        }
    }
}
