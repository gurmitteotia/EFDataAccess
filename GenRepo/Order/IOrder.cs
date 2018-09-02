using System.Linq;

namespace GenRepo
{
    public interface IOrder<T>
    {
        IOrderedQueryable<T> Apply(IQueryable<T> queryable);
    }
}