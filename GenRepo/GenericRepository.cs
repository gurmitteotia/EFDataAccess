using System;
using System.Data.Entity;
using System.Linq;

namespace GenRepo
{
    public class GenericRepository : IRepository
    {
        private readonly IDataContext _dataContext;
        public GenericRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Attach<T>(T entity) where T : class
        {
            _dataContext.Collection<T>().Attach(entity);
        }
        public void Add<T>(T entity) where T : class
        {
            _dataContext.Collection<T>().Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _dataContext.Collection<T>().Remove(entity);
        }
        public T FindByKey<T>(object key) where T : class
        {
            return _dataContext.Collection<T>().Find(key);
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
            return _dataContext.Collection<T>();
        }
        public IQueryable<T> Get<T>(IQuery<T> query) where T : class
        {
            return query.Filter(_dataContext.Collection<T>());
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