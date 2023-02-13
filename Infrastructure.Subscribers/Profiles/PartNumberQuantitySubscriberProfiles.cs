using Application.Commands;
using AutoMapper;
using Infrastructure.Subscribers.Messages;

namespace Infrastructure.Subscribers.Profiles
{
    public class PartNumberQuantitySubscriberProfiles : Profile
    {
        public PartNumberQuantitySubscriberProfiles()
        {
            CreateMap<PartNumberQuantityMessage, PartNumberQuantityCommand>();
        }
    }
}
