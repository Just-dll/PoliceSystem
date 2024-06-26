using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PoliceProject.IdentityService.Entities;

namespace PoliceProject.IdentityService.Data
{
    public class PoliceProjectIdentityContext : IdentityDbContext<User, Position, int>
    {
        public PoliceProjectIdentityContext()
        {
        }

        public PoliceProjectIdentityContext(DbContextOptions<PoliceProjectIdentityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
