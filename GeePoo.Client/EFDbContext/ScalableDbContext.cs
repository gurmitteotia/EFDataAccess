using System.Data.Entity;
using System.Reflection;

namespace GeePoo.Client.EFDbContext
{
    public class ScalableDbContext : DbContext
    {
        private readonly string _mapperAssemblyPath;
        public ScalableDbContext(string mapperAssemblyPath, string connectionName):base(connectionName)
        {
            _mapperAssemblyPath = mapperAssemblyPath;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var loadAssembly = Assembly.LoadFrom(_mapperAssemblyPath);

            modelBuilder.Configurations.AddFromAssembly(loadAssembly);
        }
    }
}