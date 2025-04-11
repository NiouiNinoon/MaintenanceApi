using Microsoft.AspNetCore.Identity;
using MaintenanceApi.Models;

namespace MaintenanceApi.Data;

public class DbSeeder
{
    public static async Task SeedAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        string[] roles = ["Admin", "Technician", "Client"];
        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        var admin = new AppUser { UserName = "admin", Email = "admin@mail.com" };
        if (await userManager.FindByNameAsync(admin.UserName) == null)
        {
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        if (!context.Clients.Any())
            context.Clients.Add(new Client { Name = "Client A" });

        if (!context.ServiceTypes.Any())
            context.ServiceTypes.Add(new ServiceType { Name = "Chauffage" });

        if (!context.Materials.Any())
        {
            context.Materials.Add(new Material { Name = "Tuyau" });
            context.Materials.Add(new Material { Name = "Thermostat" });
        }

        if (!context.Users.Any(u => u.UserName == "tech"))
        {
            var tech = new AppUser { UserName = "tech", Email = "tech@mail.com" };
            await userManager.CreateAsync(tech, "Tech123!");
            await userManager.AddToRoleAsync(tech, "Technician");
        }

        await context.SaveChangesAsync();
    }
}
