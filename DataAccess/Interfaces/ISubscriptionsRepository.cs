using Domain.Models;

namespace DataAccess.Interfaces;

public interface ISubscriptionsRepository
{
    Task<List<Subscription>> GetSubscriptionsByUserId(Guid id);
}