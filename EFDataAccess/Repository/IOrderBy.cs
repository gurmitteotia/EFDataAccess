using System.Linq;

namespace EFDataAccess.Repository
{
    public interface IOrderBy<T>
    {
        IOrderedQueryable<T> ApplyOrder(IQueryable<T> queryable);
    }
}