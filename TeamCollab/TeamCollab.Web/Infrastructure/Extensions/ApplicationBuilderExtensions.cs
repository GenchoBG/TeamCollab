using System;
using System.Collections.Generic;
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

                        if (!await db.Projects.AnyAsync())
                        {
                            var testManager = new User()
                            {
                                UserName = "TestManager",
                                Email = "manager@gmail.com"
                            };

                            var testWorker = new User()
                            {
                                UserName = "TestWorker",
                                Email = "worker@gmail.com"
                            };

                            await userManager.CreateAsync(testManager, "test12");
                            await userManager.AddToRoleAsync(testManager, "Manager");
                            await userManager.CreateAsync(testWorker, "test12");

                            var project = new Project()
                            {
                                Heading = "Test",
                                Description = "Testtesttesttest",
                                Manager = testManager,
                                Workers = new List<UserProject>()
                                {
                                    new UserProject() { User = testWorker },
                                    new UserProject() { User = testManager }
                                }
                            };

                            await db.Projects.AddAsync(project);
                            await db.SaveChangesAsync();
                        }
                    }).Wait();
            }

            return app;
        }
    }
}