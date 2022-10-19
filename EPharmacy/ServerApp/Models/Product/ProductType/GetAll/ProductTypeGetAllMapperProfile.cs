using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Product.ProductType.GetAll
{
    public class ProductTypeGetAllMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<Data.Entities.Products.ProductType, ProductTypeModel>(MemberList.Destination);
        }
    }
}
