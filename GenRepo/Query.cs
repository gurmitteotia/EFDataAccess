using System;
using System.Linq;

namespace GenRepo
{
    public class Query<T> : IQuery<T>
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

        public IQuery<T> OrderBy(Func<Order<T>, IOrder<T>> orderFunc)
        {
            return new OrderedQuery<T>(this, orderFunc(new Order<T>()));
        }
        //public QueryProjection<T, TProjection> Project<TProjection>(Expression<Func<T, TProjection>> projection)
        //{
        //    return new QueryProjection<T, TProjection>(this, projection);
        //}
    }
}