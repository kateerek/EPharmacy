using AutoMapper;
using EPharmacy.Data.Entities;
using EPharmacy.Data.Entities.Users;
using EPharmacy.ServerApp.Common.Interfaces;
using EPharmacy.ServerApp.Models.Account.Requests;
using EPharmacy.ServerApp.Models.Account.Responses;

namespace EPharmacy.ServerApp.Models.Account
{
    public class AccountModelsProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {
            CreateMap<RegistrationRequest, ApplicationUser>(MemberList.Source)
                .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.Email));
        }
        protected override void CreateMappingsForResponses()
        {
            CreateMap<string, LoginResponse>()
                .ForMember(d => d.Token, opt => opt.MapFrom(src => src));
            CreateMap<UserData, ApplicationUser>(MemberList.Source)
                .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
