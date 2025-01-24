using PublicRecords.Domain.Interface.Repository;
using PublicRecords.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PublicRecords.Infraestructure.Repository
{
    internal class BaseRepository<TEntity>(OfficialDiaryDbContext context) : IBaseRepository<TEntity> where TEntity : class
    {
        protected OfficialDiaryDbContext _context { get; } = context;

        public void AddOrUpdate(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).AsQueryable();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().AnyAsync(predicate);
        }

        public Task<int> ExecuteDeleteAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return GetAll(predicate)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
