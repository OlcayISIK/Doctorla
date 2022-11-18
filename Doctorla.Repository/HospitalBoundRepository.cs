using Doctorla.Data;
using Doctorla.Data.EF;
using Doctorla.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository
{
    public abstract class HospitalBoundRepository<TEntity> : IHospitalBoundRepository<TEntity> where TEntity : HospitalBoundEntity, new()
    {
        protected readonly Context Context;

        protected HospitalBoundRepository(Context context)
        {
            Context = context;
        }

        public IQueryable<TEntity> GetAll(long? hospitalId = null)
        {
            return Context.Set<TEntity>().Where(x => !x.IsDeleted && x.HospitalId == (hospitalId ?? Context.HospitalId));
        }

        public IQueryable<TEntity> Get(long id)
        {
            return Context.Set<TEntity>().Where(x => !x.IsDeleted && x.Id == id && x.HospitalId == Context.HospitalId);
        }

        public TEntity AddWithoutSettingCompanyId(TEntity entity)
        {
            entity.CreatedAt = Context.Now;
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public IQueryable<TEntity> GetAsTracking(long id)
        {
            return Context.Set<TEntity>().Where(x => !x.IsDeleted && x.Id == id && x.HospitalId == Context.HospitalId).AsTracking();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).Where(x => !x.IsDeleted && x.HospitalId == Context.HospitalId);
        }

        public IEnumerable<TEntity> AddRangeWithoutSettingCompanyId(IEnumerable<TEntity> entities)
        {
            var list = entities.ToList();
            foreach (var companyBoundEntity in list)
            {
                companyBoundEntity.CreatedAt = Context.Now;
            }
            Context.Set<TEntity>().AddRange(list);
            return list;
        }

        public IQueryable<TEntity> WhereInAllCompanies(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).Where(x => !x.IsDeleted);
        }

        public virtual TEntity Add(TEntity entity)
        {
            SetDefaultProperties(entity, DateTime.UtcNow, true);
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            var list = entities.ToList();
            SetDefaultProperties(list, DateTime.UtcNow, true);
            Context.Set<TEntity>().AddRange(list);
            return list;
        }

        protected void SetDefaultProperties(TEntity entity, DateTime dateTime, bool updateCreationProperties)
        {
            entity.HospitalId = Context.HospitalId;
            if (updateCreationProperties)
            {
                entity.CreatedAt = dateTime;
            }
            entity.LastModifiedAt = dateTime;
        }

        protected void SetDefaultProperties(IEnumerable<TEntity> entities, DateTime dateTime, bool updateCreationProperties)
        {
            foreach (var entity in entities)
            {
                SetDefaultProperties(entity, dateTime, updateCreationProperties);
            }
        }

        public void Remove(long id)
        {
            var entry = Context.Attach(new TEntity { Id = id });
            entry.Property(x => x.IsDeleted).IsModified = true;
            entry.Property(x => x.IsDeleted).CurrentValue = true;
            entry.Property(x => x.LastModifiedAt).IsModified = true;
            entry.Property(x => x.LastModifiedAt).CurrentValue = Context.Now;
        }

        public void BatchUpdate(BatchDto<TEntity> batch)
        {
            if (batch == null) return;
            if (batch.AddedItems != null)
                AddRange(batch.AddedItems);
            if (batch.UpdatedItems != null)
                UpdateRange(batch.UpdatedItems);
            if (batch.RemovedItems != null)
                RemoveRange(batch.RemovedItems);
        }

        // test these
        private void UpdateRange(List<TEntity> entities)
        {
            if (entities == null) return;
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                SetDefaultProperties(entities, now, false);
                var entry = Context.Set<TEntity>().Update(entity);
                entry.Property(x => x.CreatedAt).IsModified = false;
            }
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

        public void HardRemove(long id)
        {
            Context.Set<TEntity>().Remove(new TEntity { Id = id });
        }

        public void HardRemoveRange(IEnumerable<long> ids)
        {
            Context.Set<TEntity>().RemoveRange(ids.Select(x => new TEntity { Id = x }));
        }
    }
}
