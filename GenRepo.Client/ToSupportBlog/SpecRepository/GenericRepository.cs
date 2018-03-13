using System.Data.Entity;
using System.Linq;

namespace GenRepo.Client.ToSupportBlog.SpecRepository
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
        public void Add<T>(T entity) where T :class
        {
            _dataContext.Collection<T>().Add(entity);
        }
        public void Delete<T>(T entity) where T: class
        {
            _dataContext.Collection<T>().Remove(entity);
        }
        public T FindByKey<T>(object key) where T:class
        {
            return _dataContext.Collection<T>().Find(key);
        }
        public int Count<T>(Specification<T> specification) where T : class
        {
            return  Get(specification).Count();
        }
        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dataContext.Collection<T>();
        }
        public IQueryable<T> Get<T>(Specification<T> specification) where T : class
        {
            return specification.Filter(_dataContext.Collection<T>());
        }
        public IQueryable<T> Get<T>(Specification<T> specification, int pageIndex, int pageSize) where T : class
        {
            int skipItems = pageIndex * pageSize;
            return Get(specification).Skip(skipItems).Take(pageSize).AsNoTracking();
        }
        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}