using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EPharmacy.Data.Entities.ActiveSubstances;
using EPharmacy.Data.Entities.Attributes;
using EPharmacy.Data.Entities.Attributes.Configurations;
using EPharmacy.Data.Entities.Discounts;
using EPharmacy.Data.Entities.Discounts.Configurations;
using EPharmacy.Data.Entities.Products;
using EPharmacy.Data.Entities.Products.Configurations;
using EPharmacy.Data.Entities.SalesOrders;
using EPharmacy.Data.Entities.SalesOrders.Configurations;
using EPharmacy.Data.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Data
{
    public class EPharmacyContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        string,
        IdentityUserClaim<string>,
        ApplicationUserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>>
    {
        public EPharmacyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInformation> ProductInformation { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributesValues { get; set; }
        public DbSet<PrescriptionInformation> PrescriptionInformation { get; set; }        
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<PharmacyLocation> PharmacyLocations { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<DiscountCategory> DiscountCategories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<AttributeDiscount> AttributeDiscounts { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<FavouriteProduct> FavouriteProducts { get; set; }
        public DbSet<ActiveSubstance> ActiveSubstances { get; set; }
        public DbSet<ProductActiveSubstance> ProductActiveSubstances { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ///TODO
            ///To be removed leter.
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        private void OnModelCreatingImpl(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new ProductTypeConfiguration());
            builder.ApplyConfiguration(new ProductAttributeValueConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new FavouriteProductConfiguration());

            builder.ApplyConfiguration(new AttributeConfiguration());
            builder.ApplyConfiguration(new SalesOrderConfiguration());

            builder.ApplyConfiguration(new DiscountConfiguration());
            builder.ApplyConfiguration(new AttributeDiscountConfiguration());
            builder.ApplyConfiguration(new ProductDiscountConfiguration());
            builder.ApplyConfiguration(new ProductActiveSubstanceConfiguration());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            OnModelCreatingImpl(builder);
            base.OnModelCreating(builder);
        }

        #region Overriden mebers
        /*This override will allow to make kind of workaround for temporary model of
        attributes where all of them have only, 'true' and 'false' values.
        */
        public override int SaveChanges()
        {
            foreach (var entity in ChangeTracker.Entries<Attribute>().ToList()
                .Where(e => e.State == EntityState.Added))
            {
                entity.Collection("AttributeValues").CurrentValue = new List<AttributeValue>()
                {
                    new AttributeValue()
                    {
                        Value = "DefaultValue"
                    },
                };
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entity in ChangeTracker.Entries<Attribute>().ToList()
                .Where(e => e.State == EntityState.Added))
            {
                entity.Collection("AttributeValues").CurrentValue = new List<AttributeValue>()
                {
                    new AttributeValue()
                    {
                        Value = "DefaultValue"
                    },
                };
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        #endregion

    }
}