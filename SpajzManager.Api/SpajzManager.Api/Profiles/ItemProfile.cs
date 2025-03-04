﻿using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace SpajzManager.Api.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Entities.Item, Models.ItemDto>().ReverseMap();
            CreateMap<Entities.Item, Models.ItemForUpdateDto>().ReverseMap();
            CreateMap<Models.ItemForCreationDto, Entities.Item>();
        }
    }
}
