using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data.Entities.SalesOrders;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Pharmacy.AddLocation
{
    public class PharmacyAddLocationMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<PharmacyLocationRequest, PharmacyLocation>(MemberList.None);
        }

        protected override void CreateMappingsForResponses()
        {
            
        }
    }
}
