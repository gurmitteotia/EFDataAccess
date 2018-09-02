using System;
using System.Linq.Expressions;

namespace GenRepo
{
    public class Specification<T>
    {
        private Func<T, bool> _compiledExpression;
        private readonly Expression<Func<T, bool>> _expression;

        protected Specification()
        {
            _expression = t=>false;
        }
        public Specification(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public bool IsSatisfiedBy(T obj)
        {
            return CachedCompile().Invoke(obj);
        }
        public Specification<T> And(Specification<T> other)
        {
            var left = Expression();
            var right = other.Expression();

            var leftAndRight = left.And(right);
            return new Specification<T>(leftAndRight);
        }
        public Specification<T> Or(Specification<T> other)
        {
             var left = Expression();
            var right = other.Expression();

            var leftOrRight = left.Or(right);
            return new Specification<T>(leftOrRight);
        }

        protected virtual Expression<Func<T, bool>> Expression()
        {
            return _expression;
        }

        public static implicit operator Query<T>(Specification<T> spec)
        {
            return new Query<T>(new Filter<T>(spec._expression));
        }
        private Func<T, bool> CachedCompile()
        {
            return _compiledExpression ?? (_compiledExpression = Expression().Compile());
        }
    }
}