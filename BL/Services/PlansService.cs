using BL.Interfaces;
using DataAccess.Interfaces;
using Domain.Models;

namespace BL.Services;

public class PlansService : IPlansService
{
    private readonly IRepository _repository;
    
    public PlansService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Plan>> GetAllPlans()
    {
        return await _repository.GetAll<Plan>();
    }
}