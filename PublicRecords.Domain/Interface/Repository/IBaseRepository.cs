using System.Linq.Expressions;

namespace PublicRecords.Domain.Interface.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void AddOrUpdate(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> ExecuteDeleteAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);
    }
}
