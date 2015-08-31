using System.Linq;

namespace EFDataAccess.Repository
{
    public interface IOrderOf<T>
    {
        IOrderedQueryable<T> ApplyOrder(IQueryable<T> queryable);
    }
}