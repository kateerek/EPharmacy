using EPharmacy.ServerApp.Common.Interfaces;

namespace EPharmacy.ServerApp.Models.Storage.UploadFile
{
    public class UploadFileModelsProfile : ModelsProfile
    {
        protected override void CreateMappingsForRequests()
        {            
        }

        protected override void CreateMappingsForResponses()
        {
            CreateMap<string, UploadFileResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src));
        }
    }
}