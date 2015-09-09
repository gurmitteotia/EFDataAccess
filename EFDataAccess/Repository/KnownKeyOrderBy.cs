using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFDataAccess.Repository
{
    internal class KnownKeyOrderBy<T, TKey> : IOrderBy<T>
    {
        private readonly bool _ascending;
        private readonly Expression<Func<T, TKey>> _orderBy;

        public KnownKeyOrderBy(Expression<Func<T, TKey>> orderBy, bool ascending)
        {
            _ascending = ascending;
            _orderBy = orderBy;
        }
        public IOrderedQueryable<T> ApplyOrder(IQueryable<T> queryable)
        {
            if (_ascending)
            {
                return queryable.OrderBy(_orderBy);
            }
            return queryable.OrderByDescending(_orderBy);
        }
    }
}