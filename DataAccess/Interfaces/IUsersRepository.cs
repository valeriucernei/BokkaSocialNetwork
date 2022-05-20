using Domain.Models.Auth;

namespace DataAccess.Interfaces;

public interface IUsersRepository
{
    Task<bool> IsUserPaidUser(User user);
}