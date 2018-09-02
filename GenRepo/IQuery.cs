using System.Linq;

namespace GenRepo
{
    public interface IQuery<T>
    {
        IQueryable<T> Execute(IQueryable<T> items);
    }
}