using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Data.Entities.Products.Configurations
{
    public class FavouriteProductConfiguration : IEntityTypeConfiguration<FavouriteProduct>
    {
        public void Configure(EntityTypeBuilder<FavouriteProduct> builder)
        {
            builder.HasKey(key => new {key.ApplicationUserId, key.ProductId});
            builder.HasOne(x => x.Product)
                .WithMany(x => x.FavouriteProducts)
                .HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.User)
                .WithMany(x => x.FavouriteProducts)
                .HasForeignKey(x => x.ApplicationUserId);
        }
    }
}