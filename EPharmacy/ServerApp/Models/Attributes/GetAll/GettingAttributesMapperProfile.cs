using AutoMapper;
using EPharmacy.Data.Entities.Attributes;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Attributes.GetAll
{
    public class GettingAttributesMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<Attribute, AttributeResponseModel>(MemberList.Destination);
        }
    }
}