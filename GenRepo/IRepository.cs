using System.Collections.Generic;
using System.Linq;

namespace GenRepo
{
    public interface IRepository
    {
        void Attach<T>(T entity) where T : class;

        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        T FindByKey<T>(object key) where T : class;

        T FirstOrDefault<T>(IQuery<T> query) where T : class;

        int Count<T>(IQuery<T> query) where T : class;

        IEnumerable<T> Get<T>(IFilter<T> filter) where T : class;

        IEnumerable<T> Get<T>(IQuery<T> query) where T : class;

        IEnumerable<T> Get<T>(IQuery<T> query, int pageIndex, int pageSize) where T : class;

        IEnumerable<TProjection> Get<T, TProjection>(QueryProjection<T, TProjection> projection) where T : class;
        void Save();
    }
}