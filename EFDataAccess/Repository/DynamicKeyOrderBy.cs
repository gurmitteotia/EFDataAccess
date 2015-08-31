using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EFDataAccess.Repository
{
    public class DynamicKeyOrderBy<T> : IOrderBy<T>
    {
        private readonly string _propertyName;
        private readonly bool _ascending;
        public DynamicKeyOrderBy(string propertyName, bool ascending)
        {
            _ascending = ascending;
            _propertyName = propertyName;
        }
        public IOrderedQueryable<T> ApplyOrder(IQueryable<T> queryable)
        {
            //Create parameter named "o"
            var parameter = Expression.Parameter(typeof(T), "o");
            var propertyInfo = typeof(T).GetProperty(_propertyName);

            //Access the property o.ProerptyName e.g. o.Category
            var property = Expression.Property(parameter, propertyInfo);

            //Create lamdba expression o=>o.Property e.g. o=>o.Category
            var lamdbaDelegate = typeof(Func<,>).MakeGenericType(typeof(T), propertyInfo.PropertyType);
            var lambdaExpression = Expression.Lambda(lamdbaDelegate, property, parameter);

            MethodInfo method;
            //Find the OrderBy or OrderByDecending method accepting two arguments, there are overloads which accept more than two arguments.
            if (_ascending)
            {
                method = typeof(Queryable).GetMethods().Single(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            }
            else
            {
                method = typeof(Queryable).GetMethods().Single(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            }

            //Costruct the method by providing both generic types- TSource and TKey.
            var methodName = method.MakeGenericMethod(typeof(T), propertyInfo.PropertyType);

            //Call the static method - OrderBy/OrderByDescending by passing two arguments- orginal IQueryable<T> (in form of expression) and sorting expression
            var callOrderBy = Expression.Call(methodName, queryable.Expression, lambdaExpression);

            //Generate new query out of this expression.
            return (IOrderedQueryable<T>)queryable.Provider.CreateQuery(callOrderBy);
        }
    }
}