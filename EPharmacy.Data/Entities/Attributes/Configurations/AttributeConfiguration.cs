using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Data.Entities.Attributes.Configurations
{
    public class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.HasIndex(x => x.InternalName)
                .IsUnique();
            builder.HasMany(x => x.AttributeValues)
                .WithOne(x => x.Attribute)
                .OnDelete(DeleteBehavior.Cascade);            
        }
    }
}