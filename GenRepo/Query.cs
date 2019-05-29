using System;
using System.Linq;
using System.Linq.Expressions;

namespace GenRepo
{
    internal class Query<T> : IQuery<T>
    {
        private readonly IFilter<T> _filter;
        public Query(IFilter<T> filter)
        {
            _filter = filter;
        }
       
        public IQueryable<T> Execute(IQueryable<T> items)
        {
            return _filter.Apply(items);
        }
    }

    public static class QueryExtension
    {
        public static IQuery<T> OrderBy<T>(this IQuery<T> query, Func<Order<T>, IOrder<T>> orderFunc)
        {
            return new OrderedQuery<T>(query, orderFunc(new Order<T>()));
        }

        public static ProjectedQuery<T,TProjection> ToProjection<T,TProjection>(this IQuery<T> query, Expression<Func<T, TProjection>> projection)
        {
            return new ProjectedQuery<T, TProjection>(query, projection);
        }
    }

    public static class Query
    {
        public static IQuery<T> WithFilter<T>(Filter<T> filter)
         => new Query<T>(filter);

        public static IQuery<T> Everything<T>() => WithFilter(Filter<T>.Nothing);
    }
}