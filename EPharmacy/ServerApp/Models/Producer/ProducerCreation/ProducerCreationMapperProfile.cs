using AutoMapper;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Producer.ProducerCreation
{
    public class ProducerCreationMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<ProducerCreationRequestModel, Data.Entities.Products.Producer>(MemberList.Source);
        }

        protected override void CreateMappingsForResponses()
        {
        }
    }
}
