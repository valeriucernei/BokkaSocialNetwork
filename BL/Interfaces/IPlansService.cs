using Common.Dtos.Plan;

namespace BL.Interfaces;

public interface IPlansService
{
    Task<List<PlanDto>> GetAllPlans();
}