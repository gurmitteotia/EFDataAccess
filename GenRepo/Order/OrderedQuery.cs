using System.Linq;

namespace GenRepo
{
    internal class OrderedQuery<T> : IQuery<T>
    {
        private readonly IQuery<T> _query;
        private readonly IOrder<T> _order;

        public OrderedQuery(IQuery<T> query, IOrder<T> order)
        {
            _query = query;
            _order = order;
        }

        public IQueryable<T> Execute(IQueryable<T> items)
        {
            return _order.Apply(_query.Execute(items));
        }
    }
}