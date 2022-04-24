using AutoMapper;
using Common.Dtos.Subscription;
using Domain.Models;

namespace BL.Profiles;

public class SubscriptionsProfile : Profile
{
    public SubscriptionsProfile()
    {
        CreateMap<Subscription, SubscriptionDto>();
        CreateMap<SubscriptionCreateDto, Subscription>();
    }
}