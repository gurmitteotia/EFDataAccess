using System;
using System.Linq;
using System.Linq.Expressions;

namespace GenRepo
{
    internal class KnownKeyOrder<T, TKey> : IOrder<T>
    {
        private readonly bool _ascending;
        private readonly Expression<Func<T, TKey>> _orderBy;

        public KnownKeyOrder(Expression<Func<T, TKey>> orderBy, bool ascending)
        {
            _ascending = ascending;
            _orderBy = orderBy;
        }
        public IOrderedQueryable<T> Apply(IQueryable<T> queryable)
        {
            if (_ascending)
            {
                return queryable.OrderBy(_orderBy);
            }
            return queryable.OrderByDescending(_orderBy);
        }
    }
}