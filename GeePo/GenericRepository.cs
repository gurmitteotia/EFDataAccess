using System;
using System.Data.Entity;
using System.Linq;

namespace GeePo
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

        public T FirstOrDefault<T>(Specification<T> specification) where T : class
        {
            return Get(specification).FirstOrDefault();
        }

        public T FirstOrDefault<T>(Specification<T> specification, Func<Order<T>, IOrderBy<T>> orderFunc) where T : class
        {
            return Get(specification, orderFunc).FirstOrDefault();
        }

        public int Count<T>(Specification<T> specification) where T : class
        {
            return Get(specification).Count();
        }
        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dataContext.Collection<T>();
        }

        public IQueryable<T> GetAll<T>(Func<Order<T>, IOrderBy<T>> orderFunc) where T : class
        {
            var orderBy = orderFunc(new Order<T>());
            return orderBy.ApplyOrder(_dataContext.Collection<T>());
        }

        public IQueryable<T> Get<T>(Specification<T> specification) where T : class
        {
            return specification.Filter(_dataContext.Collection<T>());
        }

        public IQueryable<T> Get<T>(Specification<T> specification, Func<Order<T>, IOrderBy<T>> orderFunc) where T : class
        {
            var filteredQuery = Get(specification);
            var order = orderFunc(new Order<T>());
            return order.ApplyOrder(filteredQuery);
        }

        public IQueryable<T> Get<T>(Specification<T> specification, int pageIndex, int pageSize) where T : class
        {
            int skipItems = pageIndex * pageSize;
            return Get(specification).Skip(skipItems).Take(pageSize).AsNoTracking();
        }
        public IQueryable<T> Get<T>(Specification<T> specification, Func<Order<T>, IOrderBy<T>> orderFunc, int pageIndex, int pageSize) where T : class
        {
            var filteredQuery = Get(specification, pageIndex, pageSize);
            var order = orderFunc(new Order<T>());
            return order.ApplyOrder(filteredQuery);
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}