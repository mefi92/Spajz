using AutoMapper;

namespace SpajzManager.Api.Profiles
{
    public class HouseholdProfile : Profile
    {
        public HouseholdProfile()
        {
            CreateMap<Entities.Household, Models.HouseholdWithoutItemsDto>();
            CreateMap<Entities.Household, Models.HouseholdDto>();
        }
    }
}
