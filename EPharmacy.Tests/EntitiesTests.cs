using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using EPharmacy.Data;
using EPharmacy.Data.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;



namespace EPharmacy.Tests
{

    public class EntitiesTests
    {
        private readonly ITestOutputHelper _output;
        private readonly DbContextOptionsBuilder<EPharmacyContext> _optionsBuilder;

        public EntitiesTests(ITestOutputHelper output)
        {
            this._output = output;
            _optionsBuilder = new DbContextOptionsBuilder<EPharmacyContext>().
                UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EPharmacy;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        [Fact]
        public void DummyTest()
        {
            // var optionsBuilder = new DbContextOptionsBuilder<EPharmacyContext>();
            //var connectionString  = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EApteka;Trusted_Connection=True;MultipleActiveResultSets=true");

            using (var context = new EPharmacyContext(_optionsBuilder.Options))
            {
                context.Users.ToList().ForEach(l =>
                {
                    _output.WriteLine(l.UserName);
                });
            }
        }

        #region Creating Data
        [Fact]
        public void CreateAttribute()
        {
            var attribute = new EPharmacy.Data.Entities.Attributes.Attribute()
            {
                Name = $"Is for diabetics?",
                InternalName = $"IsForDiabetics",
                Description = $"Defines if specific product is dedicated for diabetics."
            };

            using (var context = new EPharmacyContext(_optionsBuilder.Options))
            {
                context.Attributes.Add(attribute);
                context.SaveChanges();
            }
        }

        [Fact]
        public void CreateProduct()
        {
            using (var context = new EPharmacyContext(_optionsBuilder.Options))
            {
                var product = new Product()
                {
                    Name = $"Lek3",
                    ProductPrice = 30,
                    ProductInformation = new ProductInformation()
                    {
                        Description = "aaa",
                        RecommendedIntake = "bbb",
                        Composition = "ccc",
                        ImportantTips = "ddd",
                        InstructionForUse = "eee",
                        IndicationForUse = "fff"
                    },
                    ProductType = context.ProductTypes.First(pt => pt.Id == 1),
                    Producer = context.Producers.First(p => p.Id == 1),
                    PrescriptionInformation = context.PrescriptionInformation.FirstOrDefault(pi => pi.Id == 0),
                }; 
                var attribute1Value = context.Attributes.First(a => a.Id == 1).AttributeValues.First();
                var attribute2Value = context.Attributes.First(a => a.Id == 3).AttributeValues.First();
                product.AttributesValues = new List<ProductAttributeValue>()
                {
                    new ProductAttributeValue(product, attribute1Value),
                    new ProductAttributeValue(product, attribute2Value)
                };

                context.Products.Add(product);
                context.SaveChanges();
            };                       
        }
        #endregion

        #region Viewing Data
        [Fact]
        public void ViewAttributesWithValues()
        {
            using (var context = new EPharmacyContext(_optionsBuilder.Options))
            {
                var i = 1;
                context.Attributes.ToList().ForEach(a =>
                {
                    _output.WriteLine($">>>>>>>>>>> Attribute nr. {i++} <<<<<<<<<<<<<<<");
                    _output.WriteLine($"Attribute name: {a.Name}");
                    _output.WriteLine($"Attribute internal Name: {a.InternalName}");
                    _output.WriteLine($"Attribute description: {a.Description}");
                    _output.WriteLine($"Attribute Values:");
                    a.AttributeValues?.ForEach(av =>
                    {
                        _output.WriteLine(av.Value);
                    });
                    _output.WriteLine("");
                });
            }
        }

        #endregion

        #region Removing Data
        [Fact]
        public void RemoveAttribute()
        {
            using (var context = new EPharmacyContext(_optionsBuilder.Options))
            {
                var attributeToRemove = context.Attributes.First(a => a.InternalName == "IsForHim");
                context.Attributes.Remove(attributeToRemove);
                context.SaveChanges();
            }
        }

        [Fact]
        public void RemovingProductActiveSubstance()
        {
            using (var context = new EPharmacyContext(_optionsBuilder.Options))
            {
                var product = context.Products.FirstOrDefault(p => p.Id == 10);
                var productActiveSubstanceToRemove = product?.ProductActiveSubstances.FirstOrDefault(pas => pas.ActiveSubstanceId == 13);
                product?.ProductActiveSubstances.Remove(productActiveSubstanceToRemove);
                context.SaveChanges();
            }
        }
        #endregion

    }
}
