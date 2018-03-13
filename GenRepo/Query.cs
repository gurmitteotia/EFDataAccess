using System;
using System.Linq;
using System.Linq.Expressions;

namespace GenRepo
{
    public class Query<T> : IQuery<T>
    {
        private readonly Expression<Func<T, bool>> _filteredExpression;
        private Query(Expression<Func<T, bool>> filteredExpression)
        {
            _filteredExpression = filteredExpression;
        }

        public static Query<T> Create(Expression<Func<T, bool>> expression)
        {
            return new Query<T>(expression);
        }

        public static Query<T> Create(string propertyName, OperationType operation, object value)
        {
            return new Query<T>(ExpressionBuilder.Build<T>(propertyName, operation, value));
        }
        public IQueryable<T> Filter(IQueryable<T> items)
        {
            return items.Where(_filteredExpression);
        }

        public Query<T> And(Query<T> other)
        {
            return new Query<T>(_filteredExpression.And(other._filteredExpression));
        }

        public Query<T> Or(Query<T> other)
        {
            return new Query<T>(_filteredExpression.Or(other._filteredExpression));
        }
    }
}