using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Database;
using Web.Models;

namespace Web.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationContext _db;
    private readonly IHasher _hasher;

    public AuthService(ApplicationContext db, IHasher hasher)
    {
        _db = db;
        _hasher = hasher;
    }
    
    public async Task<Result<bool>> VerifyPasswordForLogin(string login, string password)
    {
        if (string.IsNullOrEmpty(login))
            return new("Логин не валидный");

        if (string.IsNullOrEmpty(password))
            return new("Пароль не валидный");

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (user == null)
            return new("Пользователь с таким логином не найден.");

        return _hasher.VerifyHash(user.PasswordHash, password);
    }
}