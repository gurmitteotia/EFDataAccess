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

        public TOut FirstOrDefault<TIn, TOut>(IQuery<TIn, TOut> query) where TIn : class
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
        public IEnumerable<TOut> Get<TIn, TOut>(IQuery<TIn, TOut> query) where TIn : class
        {
            return FilteredItems(query).AsEnumerable();
        }
        public IEnumerable<TOut> Get<TIn, TOut>(IQuery<TIn, TOut> query, int pageIndex, int pageSize) where TIn : class where  TOut:class 
        {
            int skipItems = pageIndex * pageSize;
            return FilteredItems(query).Skip(skipItems).Take(pageSize).AsNoTracking().AsEnumerable();
        }

        public IEnumerable<T> Get<T>(IFilter<T> filter, int pageIndex, int pageSize) where T : class
        {
            return Get(new Query<T>(filter), pageIndex, pageSize);
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        private IQueryable<TOut> FilteredItems<TIn, TOut>(IQuery<TIn, TOut> query) where TIn : class
        {
            return query.Execute(_dataContext.Set<TIn>());
        }
    }
}