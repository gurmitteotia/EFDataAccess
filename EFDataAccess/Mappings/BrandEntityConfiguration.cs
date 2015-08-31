using System.Data.Entity.ModelConfiguration;
using EFDataAccess.Model;

namespace EFDataAccess.Mappings
{
    public class BrandEntityConfiguration : EntityTypeConfiguration<Brand>
    {
        public BrandEntityConfiguration()
        {
            HasKey(b => b.Id).Property(b => b.Id).HasColumnName("BrandId");
            Property(b => b.Name).HasMaxLength(256).IsRequired();
            Property(b => b.LogoPath).HasMaxLength(1024).IsOptional();
        }
    }
}