using System.Linq;

namespace GenRepo
{
    public interface IQuery<in TIn, out TOut>
    {
        IQueryable<TOut> Execute(IQueryable<TIn> items);
    }
}