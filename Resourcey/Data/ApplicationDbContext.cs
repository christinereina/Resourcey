using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resourcey.Models;

namespace Resourcey.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> // Will users go in here? as 
    {
      public DbSet<Classroom> Classrooms { get; set; }
      public DbSet<Section> Sections { get; set; }
      public DbSet<Resource> Resources { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
