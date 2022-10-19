using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.ActiveSubstance.GetAll
{
    using ActiveSubstance = Data.Entities.ActiveSubstances.ActiveSubstance;
    public class ActiveSubstanceResponseMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<ActiveSubstance, ActiveSubstanceResponse>(MemberList.Destination);
        }
    }
}
