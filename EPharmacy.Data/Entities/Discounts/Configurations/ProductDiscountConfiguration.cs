using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Data.Entities.Discounts.Configurations
{
    public class ProductDiscountConfiguration : IEntityTypeConfiguration<ProductDiscount>
    {
        public void Configure(EntityTypeBuilder<ProductDiscount> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.DiscountId });
            builder.HasOne(x => x.Product)
                   .WithMany(b => b.ProductDiscounts)
                   .HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Discount)
                   .WithMany(c => c.ProductDiscounts)
                   .HasForeignKey(x => x.DiscountId);
        }
    }
}
