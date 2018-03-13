using System.Linq;

namespace GenRepo
{
    internal class OrderedQuery<T> : IQuery<T>
    {
        private readonly Query<T> _query;
        private readonly IOrderBy<T> _order;

        public OrderedQuery(Query<T> query, IOrderBy<T> order)
        {
            _query = query;
            _order = order;
        }

        public IQueryable<T> Filter(IQueryable<T> items)
        {
            return _order.ApplyOrder(_query.Filter(items));
        }
    }
}