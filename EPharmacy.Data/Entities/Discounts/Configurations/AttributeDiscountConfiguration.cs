using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Data.Entities.Discounts.Configurations
{
    public class AttributeDiscountConfiguration : IEntityTypeConfiguration<AttributeDiscount>
    {
        public void Configure(EntityTypeBuilder<AttributeDiscount> builder)
        {
            builder.HasKey(x => new { x.AttributeId, x.DiscountId });
            builder.HasOne(x => x.Attribute)
                   .WithMany(b => b.AttributeDiscounts)
                   .HasForeignKey(x => x.AttributeId);
            builder.HasOne(x => x.Discount)
                   .WithMany(c => c.AttributeDiscounts)
                   .HasForeignKey(x => x.DiscountId);
        }
    }
}
