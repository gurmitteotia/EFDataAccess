using System;
using System.Linq.Expressions;

namespace EFDataAccess.Repository
{
    public struct Order<T>
    {
        public KnownKeyOrderBy<T, TKey> InAscOrderOf<TKey>(Expression<Func<T, TKey>> ascOrderBy)
        {
            return new KnownKeyOrderBy<T, TKey>(ascOrderBy, true);
        }
        public KnownKeyOrderBy<T, TKey> InDescOrderOf<TKey>(Expression<Func<T, TKey>> descOrderBy)
        {
            return new KnownKeyOrderBy<T, TKey>(descOrderBy, false);
        }
        public DynamicKeyOrderBy<T> InAscOrderOf(string propertyName)
        {
            return new DynamicKeyOrderBy<T>(propertyName, true);
        }
        public DynamicKeyOrderBy<T> InDescOrderOf(string propertyName)
        {
            return new DynamicKeyOrderBy<T>(propertyName, false);
        }
        public KnownKeyOrderBy<T, TKey> InOrderOf<TKey>(Expression<Func<T, TKey>> descOrderBy, OrderType orderType)
        {
            //not validating enum
            var ascending = orderType == OrderType.Ascending;
            return new KnownKeyOrderBy<T, TKey>(descOrderBy, @ascending);
        }
        public DynamicKeyOrderBy<T> InOrderOf(string propertyName, OrderType orderType)
        {
            //not validating enum
            var ascending = orderType == OrderType.Ascending;
            return new DynamicKeyOrderBy<T>(propertyName, @ascending);
        }
    }

    public enum OrderType
    {
        Ascending,

        Descending
    }
}