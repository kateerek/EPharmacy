using EPharmacy.ServerApp.Common.Interfaces;
using Attribute = EPharmacy.Data.Entities.Attributes.Attribute;

namespace EPharmacy.ServerApp.Models.Attributes.Edit
{
    public class AttributeEditionMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<AttributeEditionRequest, Attribute>()
                .ForSourceMember(aer => aer.AttributeToEditId, options => options.DoNotValidate());
        }

        protected override void CreateMappingsForResponses()
        {
        }
    }
}

