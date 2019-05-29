using System;
using System.Linq;
using System.Linq.Expressions;

namespace GenRepo
{
    internal class Query<T> : IQuery<T, T>
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
        public static IQuery<TIn, TOut> OrderBy<TIn, TOut>(this IQuery<TIn, TOut> query, Func<Order<TOut>, IOrder<TOut>> orderFunc)
        {
            return new OrderedQuery<TIn, TOut>(query, orderFunc(new Order<TOut>()));
        }

        public static IQuery<TIn, TOut> ToProjection<TIn, TOut>(this IQuery<TIn, TIn> query, Expression<Func<TIn, TOut>> projection)
        {
            return new ProjectedQuery<TIn, TOut>(query, projection);
        }
    }

    public static class Query
    {
        public static IQuery<TIn,TIn> WithFilter<TIn>(Filter<TIn> filter)
         => new Query<TIn>(filter);

        public static IQuery<TIn, TIn> Everything<TIn>() => WithFilter(Filter<TIn>.Nothing);
    }
}