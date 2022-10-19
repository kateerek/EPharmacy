using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data.Entities.SalesOrders;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.SalesOrder.Create
{
    public class SalesOrderCreateMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<SalesOrderRequest, Data.Entities.SalesOrders.SalesOrder>(MemberList.Source)
                .BeforeMap((src, dest) =>
                {
                    dest.OrderDate = DateTime.UtcNow;
                    dest.EndDate = DateTime.MinValue;
                });
            CreateMap<ProductItemRequest, ProductItem>(MemberList.Source);
    
        }

        protected override void CreateMappingsForResponses()
        {
            
        }
    }
}
