using System;
using System.Linq.Expressions;

namespace GeePo
{
    public class GenericSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _expression;
        public GenericSpecification(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        protected override Expression<Func<T, bool>> ProvideExpression()
        {
            return _expression;
        }
    }
}