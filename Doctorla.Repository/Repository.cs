using Doctorla.Data;
using Doctorla.Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly Context Context;

        public Repository(Context context)
        {
            Context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().Where(x => !x.IsDeleted);
        }

        public IQueryable<TEntity> Get(long id)
        {
            return Context.Set<TEntity>().Where(x => !x.IsDeleted && x.Id == id);
        }

        public IQueryable<TEntity> GetAsTracking(long id)
        {
            return Context.Set<TEntity>().Where(x => !x.IsDeleted && x.Id == id).AsTracking();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).Where(x => !x.IsDeleted);
        }

        public virtual TEntity Add(TEntity entity)
        {
            SetDefaultProperties(entity, DateTime.UtcNow, true);
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        protected void SetDefaultProperties(TEntity entity, DateTime dateTime, bool updateCreationProperties)
        {
            if (updateCreationProperties)
            {
                entity.CreatedAt = dateTime;
            }
            entity.LastModifiedAt = dateTime;
        }

        public void Remove(long id)
        {
            var entry = Context.Attach(new TEntity { Id = id });
            entry.Property(x => x.IsDeleted).IsModified = true;
            entry.Property(x => x.IsDeleted).CurrentValue = true;
            entry.Property(x => x.LastModifiedAt).IsModified = true;
            entry.Property(x => x.LastModifiedAt).CurrentValue = Context.Now;
        }

        public void HardRemove(long id)
        {
            Context.Set<TEntity>().Remove(new TEntity { Id = id });
        }

        public void HardRemoveRange(IEnumerable<long> ids)
        {
            Context.Set<TEntity>().RemoveRange(ids.Select(x => new TEntity { Id = x }));
        }

        public void RemoveRange(List<TEntity> entities)
        {
            if (entities == null) return;
            var now = Context.Now;
            foreach (var entity in entities)
            {
                var entry = Context.ChangeTracker.Entries<TEntity>().FirstOrDefault(x => x.Entity.Id == entity.Id);
                if (entry == null)
                {
                    entry = Context.Attach(entity);
                }
                entry.Property(x => x.IsDeleted).IsModified = true;
                entry.Property(x => x.IsDeleted).CurrentValue = true;
                entity.LastModifiedAt = now;
            }
        }
    }
}
