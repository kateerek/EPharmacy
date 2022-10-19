using EPharmacy.ServerApp.Models.Common.Responses;
using EPharmacy.ServerApp.Common.Interfaces;
using EPharmacy.ServerApp.Exceptions;

namespace EPharmacy.ServerApp.Models.Common
{
    public class CommonMapperProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {

        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<InternalServerErrorException, InternalServerErrorResponse>();
            CreateMap<NotFoundException, NotFoundResponse>();
            CreateMap<ValidationException, BadRequestResponse>()
                .ForCtorParam("dictionary", opt => opt.MapFrom(src => src.Failures));
        }
    }
}
