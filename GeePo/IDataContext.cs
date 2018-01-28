using System.Data.Entity;

namespace GeePo
{
    public interface IDataContext
    {
        DbSet<T> Collection<T>() where T : class;
        void SaveChanges();
    }
}