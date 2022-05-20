using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Plan;
using Stripe;

namespace BL.Services;

public class PlansService : IPlansService
{
    private readonly IMapper _mapper;
    
    public PlansService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<List<PlanDto>> GetAllPlans()
    {
        var options = new ProductListOptions
        {
            Limit = 3,
            Expand = new List<string> {"data.default_price"}
        };

        var service = new ProductService();

        StripeList<Product> plans = await service.ListAsync(options);

        return _mapper.Map<List<PlanDto>>(plans);
    }
}