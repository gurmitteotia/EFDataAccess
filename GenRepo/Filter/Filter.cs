// /Copyright (c) Gurmit Teotia. Please see the LICENSE file in the project root folder for license information.

using System;
using System.Linq;
using System.Linq.Expressions;

namespace GenRepo
{
    public class Filter<T> : IFilter<T>
    {
        private readonly Expression<Func<T, bool>> _filterExpression;

        internal Filter(Expression<Func<T, bool>> filterExpression)
        {
            _filterExpression = filterExpression;
        }

        public static Filter<T> Create(Expression<Func<T, bool>> expression)
        {
            return new Filter<T>(expression);
        }

        public static Filter<T> Create(string propertyName, OperationType operation, object value)
        {
            return new Filter<T>(ExpressionBuilder.Build<T>(propertyName, operation, value));
        }

        public static readonly Filter<T> Nothing= new Filter<T>(t=>true);

        public virtual IQueryable<T> Apply(IQueryable<T> items)
        {
            return items.Where(_filterExpression);
        }

        public Filter<T> And(Filter<T> other) => new Filter<T>(_filterExpression.And(other._filterExpression));

        public Filter<T> And(Expression<Func<T, bool>> other) => And(Create(other));

        public Filter<T> And(string propertyName, OperationType operation, object value) => And(Create(propertyName, operation, value));

        public Filter<T> Or(Filter<T> other) => new Filter<T>(_filterExpression.Or(other._filterExpression));

        public Filter<T> Or(Expression<Func<T, bool>> other) => Or(Create(other));

        public Filter<T> Or(string propertyName, OperationType operation, object value) => Or(Create(propertyName, operation, value));
    }
}