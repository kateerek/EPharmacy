using AutoMapper;
using EPharmacy.Data.Entities.Products;
using EPharmacy.ServerApp.Common.Interfaces;
using EPharmacy.ServerApp.Models.Product.Details;
using EPharmacy.ServerApp.Models.Product.ProductsDetailsList;

namespace EPharmacy.ServerApp.Models.Product.Common
{
    using ProductEntity = EPharmacy.Data.Entities.Products.Product;
    public class ProductCommonMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<ProductInformationModel, ProductInformation>(MemberList.Source);
            CreateMap<ProductActiveSubstanceModel, ProductActiveSubstance>(MemberList.Source);
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<ProductInformationModel, ProductInformation>(MemberList.Source);            
            CreateMap<ProductInformation, ProductInformationModel>(MemberList.Destination);
            CreateMap<ProductEntity, ProductShortModel>(MemberList.Destination);
        }
    }
}