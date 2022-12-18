using CoworkingBooking.Data.DbContexts;
using CoworkingBooking.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoworkingBooking.Data.Repositories
{
    public class Repository<TSource> : IRepository<TSource> where TSource : class
    {
        protected readonly CoworkingDBContext dbContext;
        protected readonly DbSet<TSource> dbSet;

        public Repository(CoworkingDBContext dbContext, DbSet<TSource> dbSet)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TSource>();
        }

        public async Task<TSource> AddAsync(TSource entity)
        {
            var entry = await dbSet.AddAsync(entity);

            await dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, 
                    string[] includes = null, bool isTracking = true)
        {
            IQueryable<TSource> query = expression is null ? dbSet : dbSet.Where(expression);

            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression, string[] includes = null)
            => await GetAll(expression, includes).FirstOrDefaultAsync();

        public async Task<TSource> UpdateAsync(TSource entity)
        {
            var result = dbSet.Update(entity);

            await dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteAsync(TSource entity)
        {
            dbSet.Remove(entity);

            await dbContext.SaveChangesAsync();
        }
    }
}
