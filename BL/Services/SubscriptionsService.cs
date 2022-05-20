using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Subscription;
using Common.Exceptions;
using DataAccess.Interfaces;
using Domain.Models;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace BL.Services;

public class SubscriptionsService : ISubscriptionsService
{
    private readonly IRepository _repository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;
    private readonly UserManager<User> _userManager;

    public SubscriptionsService(
        IRepository repository, 
        ISubscriptionsRepository subscriptionsRepository,
        IMapper mapper, 
        IUsersService usersService,
        UserManager<User> userManager)
    {
        _repository = repository;
        _subscriptionsRepository = subscriptionsRepository;
        _mapper = mapper;
        _usersService = usersService;
        _userManager = userManager;
    }
    
    public async Task<SubscriptionDto?> GetSubscription(Guid id)
    {
        var subscription = await _repository.GetById<Subscription>(id);

        if (subscription is null)
            throw new NotFoundException("There is no subscription with such Id.");
        
        var result = _mapper.Map<SubscriptionDto>(subscription);

        return result;
    }
    
    public async Task<List<SubscriptionDto>> GetUsersSubscriptions(ClaimsPrincipal userClaims)
    {
        var user = await _usersService.GetUserByClaims(userClaims);
        
        var subscriptions = await _subscriptionsRepository.GetSubscriptionsByUserId(user.Id);
        
        var result = _mapper.Map<List<SubscriptionDto>>(subscriptions);

        return result;
    }
    
    public async Task<SubscriptionDto> CreateSubscription(SubscriptionCreateDto subscriptionCreateDto)
    {
        var subscription = _mapper.Map<Subscription>(subscriptionCreateDto);
        
        subscription.User = await _userManager.FindByEmailAsync(subscriptionCreateDto.Email);
        
        await _repository.Add(subscription);
        await _repository.SaveChangesAsync();
        
        var result = _mapper.Map<SubscriptionDto>(subscription);

        return result;
    }
}