using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Subscription;
using DataAccess.Interfaces;
using Domain.Models;

namespace BL.Services;

public class SubscriptionsService : ISubscriptionsService
{
    private readonly IRepository _repository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;

    public SubscriptionsService(
        IRepository repository, 
        ISubscriptionsRepository subscriptionsRepository,
        IMapper mapper, 
        IUsersService usersService)
    {
        _repository = repository;
        _subscriptionsRepository = subscriptionsRepository;
        _mapper = mapper;
        _usersService = usersService;
    }
    
    public async Task<SubscriptionDto?> GetSubscription(Guid id)
    {
        var subscription = await _repository.GetById<Subscription>(id);
    
        return _mapper.Map<SubscriptionDto>(subscription);
    }
    
    public async Task<List<SubscriptionDto>> GetUsersSubscriptions(ClaimsPrincipal userClaims)
    {
        var user = _usersService.GetUserByClaims(userClaims).Result;
        
        var subscriptions = await _subscriptionsRepository.GetSubscriptionsByUserId(user.Id);

        return _mapper.Map<List<SubscriptionDto>>(subscriptions);
    }
    
    public async Task<SubscriptionDto> CreateSubscription(SubscriptionCreateDto subscriptionCreateDto, ClaimsPrincipal userClaims)
    {
        var subscription = _mapper.Map<Subscription>(subscriptionCreateDto);
        
        subscription.StartDateTime = DateTime.Now;
        subscription.EndDateTime = DateTime.Now + TimeSpan.FromDays(30);

        subscription.User = _usersService.GetUserByClaims(userClaims).Result;
        
        _repository.Add(subscription);
        
        await _repository.SaveChangesAsync();

        return _mapper.Map<SubscriptionDto>(subscription);
    }
}