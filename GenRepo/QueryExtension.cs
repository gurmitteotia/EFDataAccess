using System;

namespace GenRepo
{
    public static class QueryExtension
    {
        public static IQuery<T> OrderBy<T>(this Query<T> query, Func<Order<T>, IOrder<T>> orderFunc)
        {
            return new OrderedQuery<T>(query, orderFunc(new Order<T>()));
        }
    }
}