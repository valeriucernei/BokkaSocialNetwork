using Domain.Models;

namespace BL.Interfaces;

public interface IPlansService
{
    Task<List<Plan>> GetAllPlans();
}