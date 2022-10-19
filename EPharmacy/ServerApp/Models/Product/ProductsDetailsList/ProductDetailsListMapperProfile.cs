using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Product.ProductsDetailsList
{
    public class ProductDetailsListMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<Data.Entities.Products.Product, ProductDetailsListModel>(MemberList.Destination)
                    .ForMember(d => d.ProductDiscounts, (opt) => { opt.Ignore(); });
        }
    }
}
