using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EPharmacy.Data.Entities.Discounts.Configurations
{
    class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(x => x.ValidFrom)
                   .HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ValidTo)
                   .HasDefaultValue(DateTime.MaxValue);
            builder.HasMany(x => x.AttributeDiscounts)
                   .WithOne(x => x.Discount)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.ProductDiscounts)
                   .WithOne(x => x.Discount)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
