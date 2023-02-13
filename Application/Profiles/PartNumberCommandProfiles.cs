using Application.Commands;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles
{
    public class PartNumberCommandProfiles : Profile
    {
        public PartNumberCommandProfiles()
        {
            CreateMap<PartNumberCommand, PartNumber>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
