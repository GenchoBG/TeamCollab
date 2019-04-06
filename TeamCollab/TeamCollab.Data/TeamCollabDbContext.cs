using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamCollab.Data.Models;

namespace TeamCollab.Data
{
    public class TeamCollabDbContext : IdentityDbContext<User>
    {
        public TeamCollabDbContext(DbContextOptions<TeamCollabDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Projects)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.UserId);

            modelBuilder
                .Entity<UserProject>()
                .HasOne(us => us.Project)
                .WithMany(p => p.Workers)
                .HasForeignKey(up => up.ProjectId);

            modelBuilder
                .Entity<UserProject>()
                .HasKey(up => new {up.UserId, up.ProjectId});

            modelBuilder
                .Entity<Project>()
                .HasOne(p => p.Manager)
                .WithMany()
                .HasForeignKey(p => p.ManagerId);
        }
    }
}
