using System;
using System.Linq.Expressions;

namespace GeePo
{
    internal static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> firstExpression, Expression<Func<T, bool>> secondExpression)
        {
            var firstExpressionParameter = firstExpression.Parameters[0];
            var parameterUpdateVisitor = new ParameterReplaceVisitor(firstExpressionParameter);
            var updatedSecondExpression = (LambdaExpression)parameterUpdateVisitor.Visit(secondExpression);
           

            var andExpression = Expression.AndAlso(firstExpression.Body, updatedSecondExpression.Body);

            return Expression.Lambda<Func<T, bool>>(andExpression, firstExpressionParameter);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> firstExpression, Expression<Func<T, bool>> secondExpression)
        {
            var firstExpressionParameter = firstExpression.Parameters[0];
            var parameterUpdateVisitor = new ParameterReplaceVisitor(firstExpressionParameter);
            var updatedSecondExpression = (LambdaExpression)parameterUpdateVisitor.Visit(secondExpression);

            var andExpression = Expression.OrElse(firstExpression.Body, updatedSecondExpression.Body);

            return Expression.Lambda<Func<T, bool>>(andExpression, firstExpressionParameter);
        }

        private class ParameterReplaceVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _replaceWithParameter;

            public ParameterReplaceVisitor(ParameterExpression replaceWithParameter)
            {
                _replaceWithParameter = replaceWithParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return _replaceWithParameter;
            }
        }
    }
}