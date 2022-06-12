using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Database;

public static class DataInitializer
{
    public static async Task InitializeAsync(ApplicationContext context)
    {
        var dbRoles = await context.Roles.ToListAsync();
        var lostRoles = RoleCodes.AllRoleCodes.Except(dbRoles.Select(u => u.Code)).ToList();
        if (lostRoles.Any())
            foreach (var lostRole in lostRoles)
                context.Roles.Add(new Role { Code = lostRole });
        await context.SaveChangesAsync();

        var adminRole = await context.Roles.FirstOrDefaultAsync(u => u.Code == RoleCodes.Admin);
        var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Login == BusyCredentials.AdminLogin);
        if (adminUser == null)
            context.Users.Add(new User
            {
                Login = BusyCredentials.AdminLogin,
                Password = BusyCredentials.AdminPassword,
                RoleId = adminRole!.Id
            });

        await context.SaveChangesAsync();
    }
}