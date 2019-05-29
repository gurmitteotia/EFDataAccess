using System.Collections.Generic;

namespace GenRepo
{
    public interface IRepository
    {
        void Attach<T>(T entity) where T : class;

        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        T FindByKey<T>(object key) where T : class;

        TOut FirstOrDefault<TIn, TOut>(IQuery<TIn, TOut> query) where TIn : class;
        T FirstOrDefault<T>(IFilter<T> filter) where T : class;

        int Count<T>(IFilter<T> query) where T : class;

        IEnumerable<TOut> Get<TIn, TOut>(IQuery<TIn,TOut> query) where TIn : class;
        IEnumerable<T> Get<T>(IFilter<T> filter) where T : class;

        IEnumerable<TOut> Get<TIn, TOut>(IQuery<TIn, TOut> query, int pageIndex, int pageSize) where TIn : class where TOut : class;
        IEnumerable<TIn> Get<TIn>(IFilter<TIn> filter, int pageIndex, int pageSize) where TIn : class;

        void Save();
    }
}