using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLL.Grpc.Identity;

namespace BLL.Services
{
    public class IdentityService : IUserService
    {
        private readonly IdentityClient identityClient;
        private readonly IMapper mapper;
        public IdentityService(IdentityClient identityClient, IMapper mapper)
        {
            this.identityClient = identityClient;
            this.mapper = mapper;
        }

        public async Task<UserModel?> GetRandomUserInRole(int userId, string roleName)
        {
            var response = await identityClient.GetRandomPersonInRoleAsync(new() { RoleName = roleName });
            if (response == null)
            {
                return null;
            }
            return mapper.Map<UserModel?>(response); 
            //return new() { Position = roleName, Id = userId, Email = response.Email, Name = response.Username };
        }

        public async Task<UserModel?> GetUserAsync(int userId)
        {
            var response = await identityClient.GetPersonAsync(new() { PersonId = userId });
            if (response == null)
            {
                return null;
            }
            return mapper.Map<UserModel?>(response);
            //return new() { Email =  response.Email, Name = response.Username, Id = userId };
        }

        public async Task<IEnumerable<string>> GetUserRoles(int userId)
        {
            var response = await identityClient.GetPersonRolesAsync(new() { PersonId = userId });

            if (response == null)
            {
                return [];
            }

            return response.Roles.Select(x => x.Name);
        }
    }
}
