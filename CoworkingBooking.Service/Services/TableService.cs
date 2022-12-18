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
    public class TableService : ITableService
    {
        private readonly IRepository<Table> repository;
        private readonly IRepository<Floor> floorRepo;
        private readonly IMapper mapper;

        public TableService(IRepository<Table> repository, IRepository<Floor> floorRepo, IMapper mapper)
        {
            this.repository = repository;
            this.floorRepo = floorRepo;
            this.mapper = mapper;
        }

        public async Task<TableDTO> CreateAsync(TableDTO tableDTO)
        {
            var Table = repository.GetAsync(expression: s => s.Index == tableDTO.Index);

            if (Table is not null)
                throw new CBException(400, "Table with this index exists");

            var branch = floorRepo.GetAsync(b => b.Id == tableDTO.FloorId);

            if (branch is null)
                throw new CBException(404, "Floor with this id noes not exist");

            Table mappedTable = mapper.Map<Table>(tableDTO);
            mappedTable.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(mappedTable);

            return tableDTO;
        }

        public async Task DeleteAsync(long id)
        {
            var Table = await repository.GetAsync(expression: s => s.Id == id);

            if (Table == null)
                throw new CBException(404, "Table not found");


            await repository.DeleteAsync(Table);
        }

        public async Task<TableDTO[]> GetAllAsync(int pageIndex, int pageSize, Expression<Func<Table, bool>> expression = null)
        {
            return await repository.GetAll(pageIndex, pageSize, expression)
                .ProjectTo<TableDTO>(mapper.ConfigurationProvider).ToArrayAsync();
        }

        public async Task<TableDTO> GetAsync(Expression<Func<Table, bool>> expression)
        {
            var table = await repository.GetAsync(expression);

            if (table is null)
                throw new CBException(404, "Table not found");

            return mapper.Map<TableDTO>(table);
        }

        public async Task<TableDTO> UpdateAsync(long id, TableDTO tableDTO)
        {
            var table = await repository.GetAsync(expression: s => s.Id == id);

            if (table is null)
                throw new CBException(404, "Table with this index does not exist");

            var branch = floorRepo.GetAsync(b => b.Id == tableDTO.FloorId);

            if (branch is null)
                throw new CBException(404, "Floor with this id noes not exist");

            Table mappedTable = mapper.Map<Table>(tableDTO);
            mappedTable.CreatedAt = table.CreatedAt;
            mappedTable.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(mappedTable);

            return tableDTO;
        }
    }
}
