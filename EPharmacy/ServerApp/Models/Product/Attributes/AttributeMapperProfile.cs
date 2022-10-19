using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data.Entities.Products;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Product.Attributes
{
    public class AttributeMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
        }

        protected override void CreateMappingsForResponses()
        {

            CreateMap<ProductAttributeValue, AttributeModel>(MemberList.None).ForMember(
                    dest => dest.Name, opt =>
                        opt.MapFrom(src => src.AttributeValue.Attribute.Name))
                .ForMember(dest => dest.InternalName, opt =>
                    opt.MapFrom(src => src.AttributeValue.Attribute.InternalName)
                )
                .ForMember(dest => dest.Description, opt =>
                    opt.MapFrom(src => src.AttributeValue.Attribute.Description)
                )
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttributeValue.Attribute.Id));
        }
    }
}
