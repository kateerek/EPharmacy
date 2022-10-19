using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data.Entities.Products;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Product.Tags
{
    public class TagMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<ProductAttributeValue, AttributeTagModel>(MemberList.Destination)
                .ForMember(atm => atm.Name, options => options.MapFrom(pav => pav.AttributeValue.Attribute.Name));
        }
    }
}
