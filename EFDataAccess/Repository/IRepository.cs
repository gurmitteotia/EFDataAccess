using System;
using System.Linq;
using EFDataAccess.Spec;

namespace EFDataAccess.Repository
{
    public interface IRepository
    {
        void Attach<T>(T entity) where T : class;

        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        T FindByKey<T>(object key) where T : class;

        T FirstOrDefault<T>(Specification<T> specification) where T : class;

        T FirstOrDefault<T>(Specification<T> specification, Func<Order<T>, IOrderOf<T>> orderBy) where T : class;

        int Count<T>(Specification<T> specification) where T : class;

        IQueryable<T> GetAll<T>() where T : class;

        IQueryable<T> Get<T>(Specification<T> specification) where T : class;

        IQueryable<T> Get<T>(Specification<T> specification, int pageIndex, int pageSize) where T : class;

        IQueryable<T> GetAll<T>(Func<Order<T>,IOrderOf<T>> orderBy) where T : class;

        IQueryable<T> Get<T>(Specification<T> specification, Func<Order<T>, IOrderOf<T>> orderBy) where T : class;

        IQueryable<T> Get<T>(Specification<T> specification, Func<Order<T>, IOrderOf<T>> orderBy, int pageIndex, int pageSize) where T : class;

        void Save();
    }
}