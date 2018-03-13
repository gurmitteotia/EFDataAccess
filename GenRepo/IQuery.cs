using System.Linq;

namespace GenRepo
{
    public interface IQuery<T>
    {
        IQueryable<T> Filter(IQueryable<T> items);
    }
}