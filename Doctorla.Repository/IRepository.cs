using Doctorla.Data;
using Doctorla.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(long id);
        IQueryable<TEntity> GetAsTracking(long id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        void BatchUpdate(BatchDto<TEntity> batch);
        void Remove(long id);
        void HardRemove(long id);
        void HardRemoveRange(IEnumerable<long> ids);
    }
}
