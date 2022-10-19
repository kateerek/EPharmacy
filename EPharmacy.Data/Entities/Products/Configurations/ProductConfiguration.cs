using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Data.Entities.Products.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.ImageUrl)
                   .HasDefaultValue(
                        "https://ziwgstorage.blob.core.windows.net/product-images/57566155-5596-4d75-9a35-faf89de02153image_placeholder.png"
                    );
            builder
                .HasMany(a => a.ProductActiveSubstances)
                .WithOne(pas => pas.Product)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
