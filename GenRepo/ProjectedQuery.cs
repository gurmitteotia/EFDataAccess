// /Copyright (c) Gurmit Teotia. Please see the LICENSE file in the project root folder for license information.

using System;
using System.Linq;
using System.Linq.Expressions;

namespace GenRepo
{
    internal class ProjectedQuery<TIn,TOut> : IQuery<TIn, TOut>
    {
        private readonly IQuery<TIn,TIn> _query;
        private readonly Expression<Func<TIn, TOut>> _projection;

        public ProjectedQuery(IQuery<TIn,TIn> query, Expression<Func<TIn,TOut>> projection)
        {
            _query = query;
            _projection = projection;
        }

        public IQueryable<TOut> Execute(IQueryable<TIn> items)
        {
            return _query.Execute(items).Select(_projection);
        }
    }
}