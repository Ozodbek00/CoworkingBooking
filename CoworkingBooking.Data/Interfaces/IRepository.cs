using System.Linq.Expressions;

namespace CoworkingBooking.Data.Interfaces
{
    public interface IRepository<TSource> where TSource: class
    {
        Task<TSource> AddAsync(TSource entity);
        IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string[] includes = null, bool isTracking = true);
        Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression, string[] includes = null);
        Task<TSource> UpdateAsync(TSource entity);
        Task DeleteAsync(TSource entity);
    }
}
