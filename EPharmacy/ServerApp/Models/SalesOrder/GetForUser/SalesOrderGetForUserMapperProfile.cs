using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data.Entities.SalesOrders;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.SalesOrder.GetForUser
{
    public class SalesOrderGetForUserMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<Data.Entities.SalesOrders.SalesOrder,SalesOrderResponse >(MemberList.None);
            CreateMap<ProductItem, ProductItemResponse>(MemberList.None)
                .ForMember(dest => dest.PrescriptionCategoryInfoModel, opt => opt.MapFrom(src => src.DiscountCategory));
        }
    }
}
