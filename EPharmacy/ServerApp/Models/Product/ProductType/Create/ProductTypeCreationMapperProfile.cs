using AutoMapper;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Product.ProductType.Create
{
    public class ProductTypeCreationMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<ProductTypeCreationRequest, Data.Entities.Products.ProductType>(MemberList.Source);
        }

        protected override void CreateMappingsForResponses()
        {
        }
    }
}
