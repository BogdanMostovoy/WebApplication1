using Microsoft.EntityFrameworkCore;
using Web.Models;
 
namespace Web.Database;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Announce> Announces { get; set; }
    public DbSet<News> News { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        string adminRoleName = "admin";
        string userRoleName = "user";

        string adminLogin = "admin";
        string adminPass = "admin";


        Role adminRole = new Role{Id = 1, Name = adminRoleName};
        Role userRole = new Role{Id = 2, Name = userRoleName};
        User adminUser = new User {Id = 1, Login = adminLogin, Password = adminPass, RoleId = adminRole.Id};


        modelBuilder.Entity<Role>().HasData(new Role[]{adminRole, userRole});
        modelBuilder.Entity<User>().HasData(new User[] {adminUser});
        base.OnModelCreating(modelBuilder);
    }
}

