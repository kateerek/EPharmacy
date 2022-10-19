using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.ActiveSubstance.Create
{
    using ActiveSubstance = Data.Entities.ActiveSubstances.ActiveSubstance;

    public class ActiveSubstanceCreationMapperProfile : ModelsProfile
    { 
        protected override void CreateMappingsForRequests()
        {
            CreateMap<ActiveSubstanceCreationRequest, ActiveSubstance>(MemberList.Source);
        }

        protected override void CreateMappingsForResponses()
        {
        }
    }
}
