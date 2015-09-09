using System;
using System.Linq.Expressions;

namespace EFDataAccess.Repository
{
    public struct Order<T>
    {
        public IOrderBy<T> InAscOrderOf<TKey>(Expression<Func<T, TKey>> ascOrderBy)
        {
            return new KnownKeyOrderBy<T, TKey>(ascOrderBy, true);
        }
        public IOrderBy<T> InDescOrderOf<TKey>(Expression<Func<T, TKey>> descOrderBy)
        {
            return new KnownKeyOrderBy<T, TKey>(descOrderBy, false);
        }
        public IOrderBy<T> InAscOrderOf(string propertyName)
        {
            return new DynamicKeyOrderBy<T>(propertyName, true);
        }
        public IOrderBy<T> InDescOrderOf(string propertyName)
        {
            return new DynamicKeyOrderBy<T>(propertyName, false);
        }
        public IOrderBy<T> InOrderOf<TKey>(Expression<Func<T, TKey>> descOrderBy, OrderType orderType)
        {
            //not validating enum
            var ascending = orderType == OrderType.Ascending;
            return new KnownKeyOrderBy<T, TKey>(descOrderBy, @ascending);
        }
        public IOrderBy<T> InOrderOf(string propertyName, OrderType orderType)
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