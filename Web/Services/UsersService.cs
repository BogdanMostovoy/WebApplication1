using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Database;
using Web.Models;

namespace Web.Services;

public class UsersService : IUsersService 
{
    private readonly ApplicationContext _db;

    public UsersService(ApplicationContext db)
    {
        _db = db;
    }
    
    public async Task<Result<User>> ByLogin(string login)
    {
        var user = await _db.Users.AsNoTracking()
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Login == login);
        if (user == null)
            return new("Пользователь с таким логином не найден");

        return user;
    }

    public Task<bool> ExistsBy(int userId) => _db.Users.AnyAsync(u => u.Id == userId);
}