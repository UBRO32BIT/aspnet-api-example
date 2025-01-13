using EventManagement_BusinessObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllByParametersAsync(Expression<Func<TEntity, bool>> filter = null);
        public Task<TEntity> GetByParametersAsync(Expression<Func<TEntity, bool>>? filter = null);
        Task<TEntity?> GetAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<int> SaveChangesAsync();
        Task<int> DeleteAsync(Guid id);
    }
}
