using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Services;

namespace Web.Database;

public static class DataInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetService<ApplicationContext>();
        var hasher = serviceProvider.GetService<IHasher>();
        if (context == null)
            throw new Exception($"Can't resolve service {nameof(ApplicationContext)}");
        if (hasher == null)
            throw new Exception($"Can't resolve service {nameof(IHasher)}");
        
        var dbRoles = await context.Roles.ToListAsync();
        var lostRoles = RoleCodes.AllRoleCodes.Except(dbRoles.Select(u => u.Code)).ToList();
        if (lostRoles.Any())
            foreach (var lostRole in lostRoles)
                context.Roles.Add(new Role { Code = lostRole });
        await context.SaveChangesAsync();

        var adminRole = await context.Roles.FirstOrDefaultAsync(u => u.Code == RoleCodes.Admin);
        var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Login == AuthConstants.AdminLogin);
        if (adminUser == null)
            context.Users.Add(new User
            {
                Login = AuthConstants.AdminLogin,
                PasswordHash = hasher.Hash(AuthConstants.AdminPassword),
                RoleId = adminRole!.Id
            });

        await context.SaveChangesAsync();
    }
}