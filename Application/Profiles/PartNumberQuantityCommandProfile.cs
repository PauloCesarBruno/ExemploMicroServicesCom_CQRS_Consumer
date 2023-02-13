using Application.Commands;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles
{
    public class PartNumberQuantityCommandProfiles : Profile
    {
        public PartNumberQuantityCommandProfiles()
        {
            CreateMap<PartNumberQuantityCommand, PartNumberQuantity>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
