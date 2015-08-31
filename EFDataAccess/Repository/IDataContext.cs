using System.Data.Entity;

namespace EFDataAccess.Repository
{
    public interface IDataContext
    {
        DbSet<T> Collection<T>() where T : class;
        void SaveChanges();
    }
}