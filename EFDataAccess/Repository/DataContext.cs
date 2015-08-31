using System.Data.Entity;

namespace EFDataAccess.Repository
{
    public class DataContext : IDataContext
    {
        private readonly DbContext _dbContext;

        public DataContext(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<T> Collection<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}