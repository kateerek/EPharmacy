using AutoMapper;
using EPharmacy.Data.Entities.Products;
using EPharmacy.ServerApp.Common.Interfaces;
using EPharmacy.ServerApp.Models.Product.Common;

namespace EPharmacy.ServerApp.Models.Product.Create
{
    public class ProductCreationMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<ProductCreationRequest, Data.Entities.Products.Product>()
                .ForSourceMember(pcr => pcr.Attributes, options => options.DoNotValidate())
                .ForSourceMember(pcr => pcr.ProducerId, options => options.DoNotValidate())
                .ForSourceMember(pcr => pcr.ProductTypeId, options => options.DoNotValidate())
                .ForSourceMember(pcr => pcr.PrescriptionInformationId, options => options.DoNotValidate())
                .ForSourceMember(pcr => pcr.PrescriptionDiscounts, options => options.DoNotValidate());
        }

        protected override void CreateMappingsForResponses()
        {
        }
    }
}

