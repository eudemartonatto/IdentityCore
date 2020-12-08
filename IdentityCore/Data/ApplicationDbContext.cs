using IdentityCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<IdentityCore.Models.ApplicationAspNetUserRoles> ApplicationAspNetUserRoles { get; set; }
        public DbSet<IdentityCore.Models.ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<IdentityCore.Models.ApplicationUser> ApplicationUsers { get; set; }


    }
}
