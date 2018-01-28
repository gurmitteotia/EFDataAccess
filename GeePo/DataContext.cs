using System.Data.Entity;

namespace GeePo
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