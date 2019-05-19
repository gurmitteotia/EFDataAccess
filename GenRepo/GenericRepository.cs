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

        public T FirstOrDefault<T>(IFilter<T> filter) where T : class
        {
            return FirstOrDefault(new Query<T>(filter));
        }

        public int Count<T>(IFilter<T> filter) where T : class
        {
            return Get(filter).Count();
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

        public IEnumerable<T> Get<T>(IFilter<T> filter, int pageIndex, int pageSize) where T : class
        {
            return Get(new Query<T>(filter), pageIndex, pageSize);
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