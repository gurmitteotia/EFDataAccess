using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GenRepo
{
    public class GenericRepository : IRepository
    {
        private readonly DbContext _dataContext;
        public GenericRepository(DbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Attach<T>(T entity) where T : class
        {
            _dataContext.Set<T>().Attach(entity);
        }
        public void Add<T>(T entity) where T : class
        {
            _dataContext.Set<T>().Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _dataContext.Set<T>().Remove(entity);
        }
        public T FindByKey<T>(object key) where T : class
        {
            return _dataContext.Set<T>().Find(key);
        }

        public T FirstOrDefault<T>(IQuery<T> query) where T : class
        {
            return Get(query).FirstOrDefault();
        }
        public int Count<T>(IQuery<T> query) where T : class
        {
            return Get(query).Count();
        }

        public IEnumerable<T> Get<T>(IFilter<T> filter) where T : class
        {
            return filter.Apply(_dataContext.Set<T>()).AsEnumerable();
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _dataContext.Set<T>().AsEnumerable();
        }
        public IEnumerable<T> Get<T>(IQuery<T> query) where T : class
        {
            return FilteredItems(query).AsEnumerable();
        }
        public IEnumerable<T> Get<T>(IQuery<T> query, int pageIndex, int pageSize) where T : class
        {
            int skipItems = pageIndex * pageSize;
            return FilteredItems(query).Skip(skipItems).Take(pageSize).AsNoTracking().AsEnumerable();
        }

        public IEnumerable<TProjection> Get<T, TProjection>(QueryProjection<T, TProjection> projection) where T:class
        {
            return projection.Evaluate(_dataContext.Set<T>());
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        private IQueryable<T> FilteredItems<T>(IQuery<T> query) where T: class
        {
            return query.Execute(_dataContext.Set<T>());
        }
    }
}