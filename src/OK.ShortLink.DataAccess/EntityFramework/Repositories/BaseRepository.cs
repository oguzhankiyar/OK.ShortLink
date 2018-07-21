using Microsoft.EntityFrameworkCore;
using OK.ShortLink.Common.Entities;
using OK.ShortLink.Core.Repositories;
using OK.ShortLink.DataAccess.EntityFramework.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OK.ShortLink.DataAccess.EntityFramework.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ShortLinkDataContext _dataContext;

        private DbSet<TEntity> Entities
        {
            get
            {
                return _dataContext.Set<TEntity>();
            }
        }

        public BaseRepository(ShortLinkDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<TEntity> FindAll()
        {
            return Entities;
        }

        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.FirstOrDefault(predicate);
        }

        public TEntity Insert(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;

            _dataContext.Entry(entity).State = EntityState.Added;
            _dataContext.SaveChanges();

            return entity;
        }

        public bool Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;

            _dataContext.Entry(entity).State = EntityState.Modified;

            return _dataContext.SaveChanges() > 0;
        }

        public bool Remove(int id)
        {
            TEntity entity = FindOne(x => x.Id == id);

            _dataContext.Entry(entity).State = EntityState.Deleted;

            return _dataContext.SaveChanges() > 0;
        }
    }
}