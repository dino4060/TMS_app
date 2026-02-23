using back_end_for_TMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace back_end_for_TMS.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : IdentityDbContext<AppUser>(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        if (builder.IsConfigured)
            return;

        var connectionString = config.GetConnectionString("Database");
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("ConnectionStrings.Database is required in appsettings.json");

        builder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed Identity Data
        var (roles, users, userRoles) = DatabaseSeeder.GenerateIdentityData();
        builder.Entity<IdentityRole>().HasData(roles);
        builder.Entity<AppUser>().HasData(users);
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
    }
}
