using System;
using System.Linq.Expressions;

namespace GenRepo
{
    public struct Order<T>
    {
        public IOrderBy<T> Asc<TKey>(Expression<Func<T, TKey>> ascOrderBy)
        {
            return new KnownKeyOrderBy<T, TKey>(ascOrderBy, true);
        }
        public IOrderBy<T> Desc<TKey>(Expression<Func<T, TKey>> descOrderBy)
        {
            return new KnownKeyOrderBy<T, TKey>(descOrderBy, false);
        }
        public IOrderBy<T> Asc(string propertyName)
        {
            return new DynamicKeyOrderBy<T>(propertyName, true);
        }
        public IOrderBy<T> Desc(string propertyName)
        {
            return new DynamicKeyOrderBy<T>(propertyName, false);
        }
    }
}