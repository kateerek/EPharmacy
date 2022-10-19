using AutoMapper;
using EPharmacy.Data.Entities.Discounts;
using EPharmacy.ServerApp.Common.Interfaces;
using EPharmacy.ServerApp.Exceptions;
using EPharmacy.ServerApp.Models.Discounts.Models;
using EPharmacy.ServerApp.Models.Discounts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts
{
    public class DiscountsMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<PrescriptionDiscountModel, Discount>()
                .ForMember(x => x.DiscountCategoryId, opt => opt.MapFrom(s => s.PrescriptionCategoryId))
                .ForMember(x => x.ValidFrom, opt => opt.MapFrom(s => DateTime.Now))
                .ForMember(x => x.ValidTo, opt => opt.MapFrom(s => DateTime.MaxValue))
                .ForMember(x => x.Value, opt => opt.MapFrom(s => ToDiscountValue(s.DiscountValue)))
                .ForMember(x => x.Percent, opt => opt.MapFrom(s => ToDiscountPercent(s.DiscountValue)));

            CreateMap<CreateOfferRequest, Discount>()
                .ForMember(x => x.ValidFrom, opt => opt.MapFrom(s => DateTime.Now))
                .ForMember(x => x.Value, opt => opt.MapFrom(s => ToDiscountValue(s.DiscountValue)))
                .ForMember(x => x.Percent, opt => opt.MapFrom(s => ToDiscountPercent(s.DiscountValue)));

            CreateMap<UpdateOfferRequest, Discount>()
                .IncludeBase<CreateOfferRequest, Discount>();
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<Discount, DiscountModel>(MemberList.Destination)
                .ForMember(d => d.Price, opt => opt.Ignore() );

            CreateMap<Discount, DiscountInfoModel>(MemberList.Destination);
            CreateMap<DiscountCategory, PrescriptionCategoryInfoModel>(MemberList.Destination);

            CreateMap<Discount, DiscountValueModel>(MemberList.None)
                .ForMember(d => d.DiscountType, opt => opt.MapFrom(s => DiscountToDiscountType(s)))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => DiscountToValue(s)));
            CreateMap<Discount, DiscountDetailsModel>(MemberList.Destination)
                .ForMember(d => d.DiscountValue, opt => opt.MapFrom(s => s));
        }

        private decimal DiscountToValue(Discount discount)
        {
            if (discount.Value != 0)
            {
                return discount.Value;
            }
            if (discount.Percent != 0)
            {
                return discount.Percent;
            }
            return 0;
        }

        private DiscountValueModel.DiscountValueType DiscountToDiscountType(Discount discount)
        {
            if (discount.Value != 0)
            {
                return DiscountValueModel.DiscountValueType.Value;
            }
            if (discount.Percent != 0)
            {
                return DiscountValueModel.DiscountValueType.Percent;
            }
            return DiscountValueModel.DiscountValueType.Value;
        }

        private decimal ToDiscountPercent(DiscountValueModel value)
        {
            return value.DiscountType == DiscountValueModel.DiscountValueType.Percent
                   ? Math.Round(value.Value, 2) : 0;
        }

        private decimal ToDiscountValue(DiscountValueModel value)
        {
            return value.DiscountType == DiscountValueModel.DiscountValueType.Value
                   ? Math.Round(value.Value, 2) : 0;
        }
    }
}
