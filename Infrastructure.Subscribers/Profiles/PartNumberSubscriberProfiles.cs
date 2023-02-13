using Application.Commands;
using AutoMapper;
using Infrastructure.Subscribers.Messages;

namespace Infrastructure.Subscribers.Profiles
{
    public class PartNumberSubscriberProfiles : Profile
    {
        public PartNumberSubscriberProfiles()
        {
            CreateMap<PartNumberMessage, PartNumberCommand>();
        }
    }
}
