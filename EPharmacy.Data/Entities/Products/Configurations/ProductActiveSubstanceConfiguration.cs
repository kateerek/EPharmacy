using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Data.Entities.Products.Configurations
{
    public class ProductActiveSubstanceConfiguration : IEntityTypeConfiguration<ProductActiveSubstance>
    {
        public void Configure(EntityTypeBuilder<ProductActiveSubstance> builder)
        {
            builder.HasAlternateKey(pas => new {pas.ProductId, pas.ActiveSubstanceId})
                .HasName("IX_IX_ProductActiveSubstanceUniquenessOfKeysPair");
        }
    }
}
