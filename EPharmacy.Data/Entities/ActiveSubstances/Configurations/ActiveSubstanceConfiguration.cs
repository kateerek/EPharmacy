using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Data.Entities.ActiveSubstances.Configurations
{
    public class ActiveSubstanceConfiguration : IEntityTypeConfiguration<ActiveSubstance>
    {
        public void Configure(EntityTypeBuilder<ActiveSubstance> builder)
        {
            builder
                .HasMany(a => a.ProductActiveSubstances)
                .WithOne(pas => pas.ActiveSubstance)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
