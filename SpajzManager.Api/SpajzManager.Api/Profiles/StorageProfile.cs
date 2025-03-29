using AutoMapper;

namespace SpajzManager.Api.Profiles
{
    public class StorageProfile : Profile
    {
        public StorageProfile()
        {
            CreateMap<Entities.Storage, Models.StorageDto>();
            CreateMap<Models.StorageForCreationDto, Entities.Storage>();
        }
    }
}
