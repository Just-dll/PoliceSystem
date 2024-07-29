using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces;

public interface IUserService
{
    Task<UserModel?> GetUserAsync(int userId);
    Task<UserModel?> GetRandomUserInRole(int userId, string roleName);
    Task<IEnumerable<string>> GetUserRoles(int userId);
}
