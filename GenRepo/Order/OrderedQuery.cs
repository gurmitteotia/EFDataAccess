using System.Linq;

namespace GenRepo
{
    internal class OrderedQuery<TIn, TOut> : IQuery<TIn, TOut>
    {
        private readonly IQuery<TIn,TOut> _query;
        private readonly IOrder<TOut> _order;

        public OrderedQuery(IQuery<TIn,TOut> query, IOrder<TOut> order)
        {
            _query = query;
            _order = order;
        }

        public IQueryable<TOut> Execute(IQueryable<TIn> items)
        {
            return _order.Apply(_query.Execute(items));
        }
    }
}