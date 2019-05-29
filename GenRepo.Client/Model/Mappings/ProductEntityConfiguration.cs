using System.Data.Entity.ModelConfiguration;

namespace GenRepo.Client.Model.Mappings
{
    public class ProductEntityConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductEntityConfiguration()
        {
            HasKey(e => e.Id).Property(e => e.Id).HasColumnName("ProductId");
            Property(e => e.Name).HasMaxLength(255).IsRequired();
            Property(p => p.Description).HasMaxLength(1024);
            Property(p => p.Category).HasMaxLength(255).IsRequired();
            Property(p => p.Price).IsRequired();
            HasRequired(p=>p.Brand).WithMany(b=>b.Products).Map(m=>m.MapKey("BrandId")).WillCascadeOnDelete(false);
        }
    }
}