using System;
using System.Linq.Expressions;

namespace GenRepo
{
    public struct Order<T>
    {
        public IOrder<T> Asc<TKey>(Expression<Func<T, TKey>> ascOrderBy)
        {
            return new KnownKeyOrder<T, TKey>(ascOrderBy, true);
        }
        public IOrder<T> Desc<TKey>(Expression<Func<T, TKey>> descOrderBy)
        {
            return new KnownKeyOrder<T, TKey>(descOrderBy, false);
        }
        public IOrder<T> Asc(string propertyName)
        {
            return DynamicKeyOrder<T>.Ascending(propertyName);
        }
        public IOrder<T> Desc(string propertyName)
        {
            return DynamicKeyOrder<T>.Descending(propertyName);
        }
    }
}