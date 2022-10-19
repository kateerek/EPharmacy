using AutoMapper;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Producer.Common
{
    public class ProducerCommonMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<EPharmacy.Data.Entities.Products.Producer, ProducerModel>(MemberList.Destination);
        }
    }
}