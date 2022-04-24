using BL.Interfaces;
using Common.Dtos.Subscription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionsService _subscriptionsService;
    
    public SubscriptionsController(ISubscriptionsService subscriptionsService)
    {
        _subscriptionsService = subscriptionsService;
    }
    
    [HttpGet("subscription/{id:guid}")]
    public async Task<IActionResult> GetSubscription(Guid id)
    {
        var result =  await _subscriptionsService.GetSubscription(id);
    
        if (result is null)
            return NotFound("There is no subscription with such Id.");
    
        return Ok(result);
    }
    
    [HttpGet("personal")]
    public async Task<IActionResult> GetPersonalSubscriptions()
    {
        var result = await _subscriptionsService.GetUsersSubscriptions(User);

        return Ok(result);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreatePost(SubscriptionCreateDto subscriptionCreateDto)
    {
        var subscriptionDto = await _subscriptionsService.CreateSubscription(subscriptionCreateDto, User);

        return CreatedAtAction(nameof(GetSubscription), new { id = subscriptionDto.Id }, subscriptionDto);
    }
}