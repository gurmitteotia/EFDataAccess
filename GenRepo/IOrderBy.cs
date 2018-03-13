using System.Linq;

namespace GenRepo
{
    public interface IOrderBy<T>
    {
        IOrderedQueryable<T> ApplyOrder(IQueryable<T> queryable);
    }
}