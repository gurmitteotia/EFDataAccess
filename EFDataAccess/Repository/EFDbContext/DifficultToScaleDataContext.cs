using System.Data.Entity;
using EFDataAccess.Model;

namespace EFDataAccess.Repository.EFDbContext
{
    public class DifficultToScaleDataContext : DbContext
    {
        public DifficultToScaleDataContext(string connectionName):base(connectionName)
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id).Property(p => p.Id).HasColumnName("ProductId");
            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(255);
            //...similary set up other properties of product


            modelBuilder.Entity<Brand>().HasKey(b => b.Id).Property(b => b.Id).HasColumnName("BrandId");
            modelBuilder.Entity<Brand>().Property(p => p.Name).HasMaxLength(255);

            //.. similary setup properties for other entities.
            
            //Add mapping for other entities in your project
        }
    }
}