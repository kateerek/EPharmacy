using EPharmacy.ServerApp.Common.Interfaces;
using EPharmacy.ServerApp.Models.Account.Requests;

namespace EPharmacy.ServerApp.Models.Product.Edit
{
    public class ProductEditionMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<ProductEditionRequest, Data.Entities.Products.Product>()
                .ForSourceMember(pcr => pcr.Attributes, options => options.DoNotValidate())
                .ForSourceMember(pcr => pcr.ProducerId, options => options.DoNotValidate())
                .ForSourceMember(pcr => pcr.ProductTypeId, options => options.DoNotValidate())
                .ForSourceMember(pcr => pcr.PrescriptionInformationId, options => options.DoNotValidate())
                .ForSourceMember(pcr => pcr.ProductActiveSubstances, options => options.DoNotValidate())
                .ForMember(p => p.ProductActiveSubstances, options => options.Ignore());
        }

        protected override void CreateMappingsForResponses()
        {
        }
    }
}

