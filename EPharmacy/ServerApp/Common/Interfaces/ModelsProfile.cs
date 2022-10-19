using AutoMapper;
using EPharmacy.Data.Entities.Attributes;
using EPharmacy.ServerApp.Extensions;
using NSwag.SwaggerGeneration.Processors;

namespace EPharmacy.ServerApp.Common.Interfaces
{
    public abstract class ModelsProfile : Profile
    {
        protected ModelsProfile()
        {
            CreateMappingsForRequests();
            CreateMappingsForResponses();
        }

        protected abstract void CreateMappingsForRequests();
        protected abstract void CreateMappingsForResponses();
    }
}
