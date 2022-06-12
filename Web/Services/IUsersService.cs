using System.Threading.Tasks;
using Web.Models;

namespace Web.Services;

public interface IUsersService
{
    Task<Result<User>> ByLogin(string login);
    Task<bool> ExistsBy(int userId);
}