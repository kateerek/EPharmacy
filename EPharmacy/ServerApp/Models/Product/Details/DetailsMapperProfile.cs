using AutoMapper;
using EPharmacy.Data.Entities.Products;
using EPharmacy.ServerApp.Common.Interfaces;
using EPharmacy.ServerApp.Common.Validators;
using EPharmacy.ServerApp.Models.Product.Edit;
using EPharmacy.ServerApp.Models.Product.Tags;

namespace EPharmacy.ServerApp.Models.Product.Details
{
    public class ProductDetailsGetRequestMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<Data.Entities.Products.Product, ProductDetailsModel>(MemberList.Destination)
                .ForMember(d => d.ProductDiscounts, (opt) => { opt.Ignore(); });
        }
    }
}