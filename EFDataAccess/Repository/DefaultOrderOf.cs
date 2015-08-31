using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFDataAccess.Repository
{
    public struct DefaultOrderOf<T, TKey> : IOrderOf<T>
    {
        private readonly bool _ascending;
        private readonly Expression<Func<T, TKey>> _orderBy;

        public DefaultOrderOf(bool ascending, Expression<Func<T, TKey>> orderBy)
        {
            _ascending = @ascending;
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