using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityService.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityService.Services;

public class PoliceIdentityProfileService : ProfileService<User>
{
    private readonly UserManager<User> userManager;
    private readonly IUserClaimsPrincipalFactory<User> principalFactory;

    public PoliceIdentityProfileService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> principalFactory) : base(userManager, principalFactory)
    {
        this.userManager = userManager;
        this.principalFactory = principalFactory;
    }
    public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);
        var roles = await userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Name, user.UserName)
        };

        claims.AddRange(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));

        context.IssuedClaims.AddRange(claims);
    }

    public override async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);
        context.IsActive = user != null && user.LockoutEnd == null;
    }
}
