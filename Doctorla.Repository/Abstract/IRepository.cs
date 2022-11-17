using Doctorla.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository.Abstract
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(long id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        IQueryable<TEntity> GetAsTracking(long id);
        void Remove(long id);
        void HardRemove(long id);
        void HardRemoveRange(IEnumerable<long> ids);
    }
}
