using AutoMapper;
using EPharmacy.ServerApp.Common.Interfaces;
using Attribute = EPharmacy.Data.Entities.Attributes.Attribute;

namespace EPharmacy.ServerApp.Models.Attributes.Create
{
    public class AttributeCreationMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<AttributeCreationRequest, Attribute>(MemberList.Source);
        }

        protected override void CreateMappingsForResponses()
        {
        }
    }
}
