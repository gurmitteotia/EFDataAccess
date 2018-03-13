using System.Data.Entity;

namespace GenRepo
{
    public interface IDataContext
    {
        DbSet<T> Collection<T>() where T : class;
        void SaveChanges();
    }
}