using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlansController : ControllerBase
{
    private readonly IPlansService _plansService;
    
    public PlansController(IPlansService plansService)
    {
        _plansService = plansService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlans()
    {
        var result = await _plansService.GetAllPlans();

        return Ok(result);
    }
}