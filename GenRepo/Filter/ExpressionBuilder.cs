using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GenRepo
{
    internal static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> Build<T>(string propertyName, OperationType operationType,
            object value)
        {
            //Create parameter expression "o"
            var parameter = Expression.Parameter(typeof(T), "o");

            //Search the member- field or property. You can also pass it BindingFlags.IgnoreCase to do case insensitive search.
            var memberInfo = typeof(T).GetMember(propertyName, MemberTypes.Property | MemberTypes.Field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();
            if (memberInfo == null)
            {
                throw new ArgumentException(string.Format("Can not find the property or field by name {0} on type {1}", propertyName, typeof(T).Name));
            }

            //Create o.MemberName expression
            var property = Expression.MakeMemberAccess(parameter, memberInfo);

            var targetType = GetTargetType(memberInfo);
            //Change the provided _value to target type.
            var targetValue = Convert.ChangeType(value, targetType);

            //Create expression for targetValue
            var valueExpression = Expression.Constant(targetValue);

            //Combin property and value expression using operation type e.g.  o.PropertyName >= targetValue
            var filterExpression = CombineExpression(property, valueExpression, operationType);

            //Generate lamda expression e.g. o=>o.PropertyName >= targetValue
            return Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
        }

        private static Type GetTargetType(MemberInfo memberInfo)
        {
            if (memberInfo.MemberType == MemberTypes.Field)
            {
                return ((FieldInfo)memberInfo).FieldType;
            }
            if (memberInfo.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)memberInfo).PropertyType;
            }

            throw new NotSupportedException(string.Format("Does not support the member type {0}", memberInfo.MemberType));
        }

        private static Expression CombineExpression(Expression left, Expression right, OperationType operationType)
        {
            switch (operationType)
            {
                case OperationType.EqualTo:
                    return Expression.Equal(left, right);
                case OperationType.NotEqualTo:
                    return Expression.NotEqual(left, right);
                case OperationType.GreaterThan:
                    return Expression.GreaterThan(left, right);
                case OperationType.GreaterThanEqualTo:
                    return Expression.GreaterThanOrEqual(left, right);
                case OperationType.LessThan:
                    return Expression.LessThan(left, right);
                case OperationType.LessThanEqualTo:
                    return Expression.LessThanOrEqual(left, right);
                case OperationType.Contains:
                    return CallMethodOnString(left, "Contains", right);
                case OperationType.StartsWith:
                    return CallMethodOnString(left, "StartsWith", right);
                case OperationType.EndsWith:
                    return CallMethodOnString(left, "EndsWith", right);
            }
            throw new NotSupportedException();
        }

        private static Expression CallMethodOnString(Expression instance, string methodName, Expression parameter)
        {
            var method = typeof(string).GetMethods().Single(m => m.Name == methodName && m.GetParameters().Count() == 1);
            return Expression.Call(instance, method, parameter);
        }
    }
}