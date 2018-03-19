using System.Linq;

namespace GenRepo
{
    internal class OrderedQuery<T> : IQuery<T>
    {
        private readonly Query<T> _query;
        private readonly IOrder<T> _order;

        public OrderedQuery(Query<T> query, IOrder<T> order)
        {
            _query = query;
            _order = order;
        }

        public IQueryable<T> Filter(IQueryable<T> items)
        {
            return _order.Apply(_query.Filter(items));
        }
    }
}