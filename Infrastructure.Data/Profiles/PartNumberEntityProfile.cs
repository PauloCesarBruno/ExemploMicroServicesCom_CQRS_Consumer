using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Profiles
{
    public class PartNumberEntityProfile : Profile
    {
        public PartNumberEntityProfile()
        {
            CreateMap<PartNumber, PartNumberEntity>()
                //.ForMember(d => d.Id, o => o.Ignore())
                //.ForMember(d => d.AliasKey, o => o.Ignore()).ReverseMap();
                .ReverseMap();
        }
    }
}
