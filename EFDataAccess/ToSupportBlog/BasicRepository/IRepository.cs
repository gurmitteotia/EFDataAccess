using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFDataAccess.ToSupportBlog.BasicRepository
{
    public interface IRepository
    {
        void Attach<T>(T entity) where T : class;

        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        T FindByKey<T>(object key) where T : class;

        int Count<T>(Expression<Func<T, bool>> whereExpression) where T : class;

        IQueryable<T> GetAll<T>() where T : class;

        IQueryable<T> Get<T>(Expression<Func<T, bool>> whereExpression) where T : class;

        IQueryable<T> Get<T>(Expression<Func<T, bool>> whereExpression, int pageIndex, int pageSize) where T : class;

        void Save();
    }
}