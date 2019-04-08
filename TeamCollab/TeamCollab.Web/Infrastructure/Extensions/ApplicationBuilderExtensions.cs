using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamCollab.Data;
using TeamCollab.Data.Models;

namespace TeamCollab.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TeamCollabDbContext>().Database.Migrate();


                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                var db = serviceScope.ServiceProvider.GetService<TeamCollabDbContext>();

                Task
                    .Run(async () =>
                    {
                        if (!await db.Roles.AnyAsync())
                        {
                            var managerRole = new IdentityRole("Manager");
                            var companyRole = new IdentityRole("Company");

                            await roleManager.CreateAsync(managerRole);
                            await roleManager.CreateAsync(companyRole);

                            var company = new User()
                            {
                                UserName = "Company",
                                Email = "company.email@company.com"
                            };
                            
                            await userManager.CreateAsync(company, "company123");

                            await userManager.AddToRoleAsync(company, "Company");
                        }
                    }).Wait();
            }

            return app;
        }
    }
}