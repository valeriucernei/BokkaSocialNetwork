using System.Security.Claims;
using Common.Dtos.Subscription;

namespace BL.Interfaces;

public interface ISubscriptionsService
{
    Task<SubscriptionDto?> GetSubscription(Guid id);
    Task<List<SubscriptionDto>> GetUsersSubscriptions(ClaimsPrincipal userClaims);
    Task<SubscriptionDto> CreateSubscription(SubscriptionCreateDto subscriptionCreateDto);
}