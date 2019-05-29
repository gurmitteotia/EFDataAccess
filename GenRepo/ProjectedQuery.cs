// /Copyright (c) Gurmit Teotia. Please see the LICENSE file in the project root folder for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenRepo
{
    public class ProjectedQuery<T,TProjection>
    {
        private readonly IQuery<T> _query;
        private readonly Expression<Func<T, TProjection>> _projection;

        public ProjectedQuery(IQuery<T> query, Expression<Func<T,TProjection>> projection)
        {
            _query = query;
            _projection = projection;
        }

        public IEnumerable<TProjection> Evaluate(IQueryable<T> items)
        {
            return _query.Execute(items).Select(_projection);
        }
    }
}