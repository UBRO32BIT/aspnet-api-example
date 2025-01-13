using EventManagement_BusinessObjects.Common;
using EventManagement_Repositories.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventManagement_Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly BaseDAO<TEntity> _dao;

        public BaseRepository(BaseDAO<TEntity> dao)
        {
            _dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        /// <inheritdoc />
        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return _dao.ExistsAsync(filter);
        }

        /// <inheritdoc />
        public Task<List<TEntity>> GetAllAsync()
        {
            return _dao.GetAllAsync();
        }

        /// <inheritdoc />
        public Task<TEntity?> GetAsync(Guid id)
        {
            return _dao.GetAsync(id);
        }

        /// <inheritdoc />
        public Task<TEntity> AddAsync(TEntity entity)
        {
            return _dao.AddAsync(entity);
        }

        /// <inheritdoc />
        public Task<int> SaveChangesAsync()
        {
            return _dao.SaveChangesAsync();
        }

        /// <inheritdoc />
        public Task<int> DeleteAsync(Guid id)
        {
            return _dao.DeleteAsync(id);
        }

        public Task<IEnumerable<TEntity>> GetAllByParametersAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            return _dao.GetAllByParametersAsync(filter);
        }

        public Task<TEntity> GetByParametersAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return _dao.GetByParametersAsync(filter);
        }
    }

}
