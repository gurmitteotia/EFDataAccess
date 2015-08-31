using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFDataAccess.Spec
{
    public class DynamicSpecification<T> : Specification<T>
    {
        private readonly string _propertyName;
        private readonly OperationType _operationType;
        private readonly object _value;

        public DynamicSpecification(string propertyName, OperationType operationType, object value)
        {
            _propertyName = propertyName;
            _value = value;
            _operationType = operationType;
        }

        protected override Expression<Func<T, bool>> ProvideExpression()
        {
            //Create parameter expression "o"
            var parameter = Expression.Parameter(typeof(T), "o");
            var propertyInfo = typeof(T).GetProperty(_propertyName);
            
            //Create o.PropertyName expression
            var property = Expression.Property(parameter, propertyInfo);

            var targetValue = Convert.ChangeType(_value, propertyInfo.PropertyType);
            
            //Create expression for targetValue
            var valueExpression = Expression.Constant(targetValue);

            //Combin property and value expression using operation type e.g.  o.PropertyName >= targetValue
            var filterExpression = CombineExpression(property, valueExpression);

            //Generate lamda expression e.g. o=>o.PropertyName >= targetValue
            return Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
        }

        private Expression CombineExpression(Expression left, Expression right)
        {
            switch (_operationType)
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

        private Expression CallMethodOnString(Expression instance, string methodName, Expression parameter)
        {
            var method = typeof(string).GetMethods().Single(m => m.Name == methodName && m.GetParameters().Count() == 1);
            return Expression.Call(instance, method, parameter);
        }
    }

    public enum OperationType
    {
        EqualTo,

        NotEqualTo,

        GreaterThan,

        GreaterThanEqualTo,

        LessThan,

        LessThanEqualTo,

        Contains,

        StartsWith,

        EndsWith
    }
}