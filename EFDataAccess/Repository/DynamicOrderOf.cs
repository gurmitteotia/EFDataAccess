using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EFDataAccess.Repository
{
    public class DynamicOrderOf<T> : IOrderOf<T>
    {
        private readonly string _propertyName;
        private readonly bool _ascending;

        public DynamicOrderOf(bool ascending, string propertyName)
        {
            _ascending = ascending;
            _propertyName = propertyName;
        }

        public IOrderedQueryable<T> ApplyOrder(IQueryable<T> queryable)
        {
            var parameter = Expression.Parameter(typeof(T), "o");
            var propertyInfo = typeof(T).GetProperty(_propertyName);
            var property = Expression.Property(parameter, propertyInfo);

            var lamdbaDelegate = typeof(Func<,>).MakeGenericType(typeof(T), propertyInfo.PropertyType);
            var lambdaExpression = Expression.Lambda(lamdbaDelegate, property, parameter);

            MethodInfo method;

            if (_ascending)
            {
                method = typeof(Queryable).GetMethods().Single(m => m.Name == "DefaultOrderOf" && m.GetParameters().Length == 2);
            }
            else
            {
                method = typeof(Queryable).GetMethods().Single(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            }

            var methodName = method.MakeGenericMethod(typeof(T), propertyInfo.PropertyType);

            var callOrderBy = Expression.Call(methodName, queryable.Expression, lambdaExpression);

            return (IOrderedQueryable<T>)queryable.Provider.CreateQuery(callOrderBy);

        }
    }
}