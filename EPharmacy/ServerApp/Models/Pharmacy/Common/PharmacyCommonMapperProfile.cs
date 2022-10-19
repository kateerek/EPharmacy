using AutoMapper;
using EPharmacy.Data.Entities.SalesOrders;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Pharmacy.Common
{
    public class PharmacyCommonMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<PharmacyLocation, PharmacyLocationModel>(MemberList.None).ReverseMap();
        }
    }
}
