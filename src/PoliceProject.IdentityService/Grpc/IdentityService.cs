using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PoliceProject.IdentityService.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PoliceProject.IdentityService.Grpc
{
    public class IdentityService : Identity.IdentityBase
    {
        private readonly ILogger<IdentityService> _logger;
        private readonly UserManager<User> userManager;
        public IdentityService(ILogger<IdentityService> logger, UserManager<User> userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }

        // [Authorize(Policy = "RequirePersonScope")]
        [AllowAnonymous]
        public override async Task<PersonResponse> GetRandomPersonInRole(GetRandomPersonInRoleRequest request, ServerCallContext context)
        {
            var usersInRole = await userManager.GetUsersInRoleAsync(request.RoleName);
            if(usersInRole.Count == 0)
            {
                return new PersonResponse();
            }
            var rnd = new Random();
            int randomIndex = rnd.Next(0, usersInRole.Count);
            var user = usersInRole[randomIndex];
            return new PersonResponse()
            {
                Email = user.Email,
                PersonId = user.Id,
                Username = user.UserName,
            };
        }

        [AllowAnonymous]
        public override async Task<PersonRolesResponse> GetPersonRoles(GetPersonRolesRequest request, ServerCallContext context)
        {
            var user = await userManager.FindByIdAsync(request.PersonId.ToString());

            if(user == null)
            {
                return new PersonRolesResponse();
            }

            var roles = (await userManager.GetRolesAsync(user)).Select(r => new Role()
            {
                Name = r,
            });

            var response = new PersonRolesResponse();
            response.Roles.AddRange(roles);
            return response;
        }
    }
}
