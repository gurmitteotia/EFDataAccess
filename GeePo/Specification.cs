using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GeePo
{
    public abstract class Specification<T>
    {
        private Func<T, bool> _compiledExpression; 
 
        public bool IsSatisfiedBy(T obj)
        {
            return CachedCompile().Invoke(obj);
        }
        public IQueryable<T> Filter(IQueryable<T> queryable)
        {
            return queryable.Where(ProvideExpression());
        }
        public IEnumerable<T> Filter(IEnumerable<T> queryable)
        {
            return queryable.Where(CachedCompile());
        }
        public Specification<T> And(Specification<T> other)
        {
            var left = ProvideExpression();
            var right = other.ProvideExpression();

            var leftAndRight = left.And(right);
            return new GenericSpecification<T>(leftAndRight);
        }
        public Specification<T> Or(Specification<T> other)
        {
             var left = ProvideExpression();
            var right = other.ProvideExpression();

            var leftOrRight = left.Or(right);
            return new GenericSpecification<T>(leftOrRight);
        }
        protected abstract Expression<Func<T, bool>> ProvideExpression();

        private Func<T, bool> CachedCompile()
        {
            return _compiledExpression ?? (_compiledExpression = ProvideExpression().Compile());
        }
    }
}