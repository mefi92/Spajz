using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace SpajzManager.Api.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Entities.Item, Models.ItemDto>();
            CreateMap<Entities.Item, Models.ItemForUpdateDto>();
            CreateMap<Models.ItemForCreationDto, Entities.Item>();
            CreateMap<Models.ItemForUpdateDto, Entities.Item>();
        }
    }
}
