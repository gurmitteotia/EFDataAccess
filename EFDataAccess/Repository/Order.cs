using System;
using System.Linq.Expressions;

namespace EFDataAccess.Repository
{
    public struct Order<T>
    {
        public DefaultOrderOf<T, TKey> InAscOrderOf<TKey>(Expression<Func<T, TKey>> ascOrderBy)
        {
            return new DefaultOrderOf<T, TKey>(true, ascOrderBy);
        }

        public DefaultOrderOf<T, TKey> InDescOrderOf<TKey>(Expression<Func<T, TKey>> descOrderBy)
        {
            return new DefaultOrderOf<T, TKey>(false, descOrderBy);
        }

        public DynamicOrderOf<T> InAscOrderOf(string propertyName)
        {
            return new DynamicOrderOf<T>(true, propertyName);
        }

        public DynamicOrderOf<T> InDescOrderOf(string propertyName)
        {
            return new DynamicOrderOf<T>(false, propertyName);
        }
    }
}