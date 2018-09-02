using System.Linq;

namespace GenRepo
{
    public interface IFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> items);
    }
}