using AutoMapper;
using Common.Dtos.Plan;
using Stripe;

namespace BL.Profiles;

public class PlanProfile : Profile
{
    public PlanProfile()
    {
        CreateMap<Product, PlanDto>()
            .ForMember(x => x.Title, y => y.MapFrom(z => z.Name))
            .ForMember(x => x.Days, y => y.MapFrom(z => z.DefaultPrice.Recurring.IntervalCount * 30))
            .ForMember(x => x.Price, y => y.MapFrom(z => z.DefaultPrice.UnitAmount / 100))
            .ForMember(x => x.PriceId, y => y.MapFrom(z => z.DefaultPrice.Id));
    }
}