﻿// <auto-generated />
using System;
using EPharmacy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EPharmacy.Data.Migrations
{
    [DbContext(typeof(EPharmacyContext))]
    [Migration("20190601123207_AddFavouriteProducts")]
    partial class AddFavouriteProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EPharmacy.Data.Entities.Attributes.Attribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("InternalName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.HasIndex("InternalName")
                        .IsUnique();

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Attributes.AttributeValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AttributeId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.ToTable("AttributeValues");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Discounts.AttributeDiscount", b =>
                {
                    b.Property<int>("AttributeId");

                    b.Property<int>("DiscountId");

                    b.HasKey("AttributeId", "DiscountId");

                    b.HasIndex("DiscountId");

                    b.ToTable("AttributeDiscounts");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Discounts.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("DiscountCategoryId");

                    b.Property<string>("Name");

                    b.Property<decimal>("Percent")
                        .HasColumnType("decimal(3, 2)");

                    b.Property<DateTime>("ValidFrom")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 6, 1, 14, 32, 6, 734, DateTimeKind.Local));

                    b.Property<DateTime>("ValidTo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("DiscountCategoryId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Discounts.DiscountCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("DiscountCategories");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Discounts.ProductDiscount", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("DiscountId");

                    b.HasKey("ProductId", "DiscountId");

                    b.HasIndex("DiscountId");

                    b.ToTable("ProductDiscounts");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.FavouriteProduct", b =>
                {
                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("ProductId");

                    b.HasKey("ApplicationUserId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("FavouriteProducts");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.PrescriptionInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("PrescriptionInformation");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("https://ziwgstorage.blob.core.windows.net/product-images/57566155-5596-4d75-9a35-faf89de02153image_placeholder.png");

                    b.Property<string>("Name");

                    b.Property<int?>("PrescriptionInformationId");

                    b.Property<int>("ProducerId");

                    b.Property<decimal>("ProductPrice")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ProductTypeId");

                    b.HasKey("Id");

                    b.HasIndex("PrescriptionInformationId");

                    b.HasIndex("ProducerId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.ProductAttributeValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttributeValueId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasAlternateKey("AttributeValueId", "ProductId")
                        .HasName("IX_UniquenessOfKeysPair");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductAttributesValues");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.ProductInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Composition")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("ImportantTips");

                    b.Property<string>("IndicationForUse")
                        .IsRequired();

                    b.Property<string>("InstructionForUse");

                    b.Property<int>("ProductId");

                    b.Property<string>("RecommendedIntake")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductInformation");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InternalName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("InternalName")
                        .IsUnique();

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.SalesOrders.PharmacyLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PharmacyLocations");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.SalesOrders.ProductItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemCount");

                    b.Property<int>("ProductId");

                    b.Property<int>("SalesOrderId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SalesOrderId");

                    b.ToTable("ProductItems");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.SalesOrders.SalesOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId")
                        .IsRequired();

                    b.Property<int>("PharmacyLocationId");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("In progress");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("PharmacyLocationId");

                    b.ToTable("SalesOrders");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Users.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Users.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Users.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<string>("RoleId1");

                    b.Property<string>("UserId1");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Attributes.AttributeValue", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Attributes.Attribute", "Attribute")
                        .WithMany("AttributeValues")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Discounts.AttributeDiscount", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Attributes.Attribute", "Attribute")
                        .WithMany("AttributeDiscounts")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EPharmacy.Data.Entities.Discounts.Discount", "Discount")
                        .WithMany("AttributeDiscounts")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Discounts.Discount", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Discounts.DiscountCategory", "DiscountCategory")
                        .WithMany()
                        .HasForeignKey("DiscountCategoryId");
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Discounts.ProductDiscount", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Discounts.Discount", "Discount")
                        .WithMany("ProductDiscounts")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EPharmacy.Data.Entities.Products.Product", "Product")
                        .WithMany("ProductDiscounts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.FavouriteProduct", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Users.ApplicationUser", "User")
                        .WithMany("FavouriteProducts")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EPharmacy.Data.Entities.Products.Product", "Product")
                        .WithMany("FavouriteProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.Product", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Products.PrescriptionInformation", "PrescriptionInformation")
                        .WithMany("Products")
                        .HasForeignKey("PrescriptionInformationId");

                    b.HasOne("EPharmacy.Data.Entities.Products.Producer", "Producer")
                        .WithMany("Products")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EPharmacy.Data.Entities.Products.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.ProductAttributeValue", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Attributes.AttributeValue", "AttributeValue")
                        .WithMany("ProductsAttributeValues")
                        .HasForeignKey("AttributeValueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EPharmacy.Data.Entities.Products.Product", "Product")
                        .WithMany("AttributesValues")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Products.ProductInformation", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Products.Product", "Product")
                        .WithOne("ProductInformation")
                        .HasForeignKey("EPharmacy.Data.Entities.Products.ProductInformation", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.SalesOrders.ProductItem", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EPharmacy.Data.Entities.SalesOrders.SalesOrder", "SalesOrder")
                        .WithMany("Items")
                        .HasForeignKey("SalesOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.SalesOrders.SalesOrder", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Users.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EPharmacy.Data.Entities.SalesOrders.PharmacyLocation", "PharmacyLocation")
                        .WithMany()
                        .HasForeignKey("PharmacyLocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EPharmacy.Data.Entities.Users.ApplicationUserRole", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Users.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EPharmacy.Data.Entities.Users.ApplicationRole", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleId1");

                    b.HasOne("EPharmacy.Data.Entities.Users.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EPharmacy.Data.Entities.Users.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Users.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Users.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Users.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EPharmacy.Data.Entities.Users.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
