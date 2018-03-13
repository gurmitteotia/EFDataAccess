using System.Data.Entity;
using System.Reflection;

namespace GenRepo.Client.EFDbContext
{
    public class ScalableDbContext : DbContext
    {
        private readonly Assembly _mapperAssembly;
        public ScalableDbContext(Assembly mapperAssembly, string connectionName):base(connectionName)
        {
            _mapperAssembly = mapperAssembly;
        }

        public static DbContext Create(string mapperAssemblyPath, string connectionName)
        {
            return new ScalableDbContext(Assembly.LoadFrom(mapperAssemblyPath), connectionName);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(_mapperAssembly);
        }
    }
}