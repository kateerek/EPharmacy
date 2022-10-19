using EPharmacy.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Data.Entities.SalesOrders.Configurations
{
    public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
    {
        public void Configure(EntityTypeBuilder<SalesOrder> builder)
        {
            builder.Property(x => x.Status)
                   .HasDefaultValue(SalesOrderStatuses.InProgress);        
        }
    }
}
