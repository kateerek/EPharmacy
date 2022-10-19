using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPharmacy.Data.Entities.Products;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Product.ActiveSubstances
{
    public class ProductActiveSubstanceResponseMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<ProductActiveSubstance, ProductActiveSubstanceResponse>()
                .ForMember(pdasm => pdasm.Id, options => options.MapFrom(pas => pas.ActiveSubstanceId))
                .ForMember(pdasm => pdasm.Name, options => options.MapFrom(pas => pas.ActiveSubstance.Name))
                .ForMember(pdasm => pdasm.InternalName, options => options.MapFrom(pas => pas.ActiveSubstance.InternalName))
                .ForMember(pdasm => pdasm.Amount, options => options.MapFrom(pas => pas.Amount));
        }
    }
}
