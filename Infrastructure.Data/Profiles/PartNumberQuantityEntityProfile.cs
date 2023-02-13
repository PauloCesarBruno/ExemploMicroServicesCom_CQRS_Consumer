using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Profiles
{
    public class PartNumberQuantityEntityProfile : Profile
    {
        public PartNumberQuantityEntityProfile()
        {
            CreateMap<PartNumberQuantity, PartNumberQuantityEntity>()
                //.ForMember(d => d.Id, o => o.Ignore())
                //.ForMember(d => d.AliasKey, o => o.Ignore())
                .ReverseMap();
        }
    }
}
