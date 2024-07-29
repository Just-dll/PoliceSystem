using IdentityService.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Extensions;

public static class RoleSeedExtension
{
    private static readonly string[] Roles = { "Policeman", "OrganizationAdministrator", "Prosecutor", "Judge", "Civilian", "Attourney" };

    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Position>>();
        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new Position()
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }
        }
    }
}
