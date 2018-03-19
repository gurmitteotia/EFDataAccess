using System;
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

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dataContext.Set<T>();
        }
        public IQueryable<T> Get<T>(IQuery<T> query) where T : class
        {
            return query.Filter(_dataContext.Set<T>());
        }
        public IQueryable<T> Get<T>(IQuery<T> query, int pageIndex, int pageSize) where T : class
        {
            int skipItems = pageIndex * pageSize;
            return Get(query).Skip(skipItems).Take(pageSize).AsNoTracking();
        }
        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}