using System.Data.Entity;
using System.Reflection;

namespace GenRepo
{
    public class AssemblyDbContext : DbContext
    {
        private readonly Assembly _mapperAssembly;
        public AssemblyDbContext(Assembly mapperAssembly, string connectionName):base(connectionName)
        {
            _mapperAssembly = mapperAssembly;
        }

        public static DbContext Create(string mapperAssemblyPath, string connectionName)
        {
            return new AssemblyDbContext(Assembly.LoadFrom(mapperAssemblyPath), connectionName);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(_mapperAssembly);
        }
    }
}