using System.Threading.Tasks;
using Web.Models;

namespace Web.Services;

public interface IAuthService
{
    /// <summary>
    /// Returns true if passwords equals
    /// </summary>
    Task<Result<bool>> VerifyPasswordForLogin(string login, string password);
}