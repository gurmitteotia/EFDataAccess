using System.Linq;

namespace GeePo
{
    public interface IOrderBy<T>
    {
        IOrderedQueryable<T> ApplyOrder(IQueryable<T> queryable);
    }
}