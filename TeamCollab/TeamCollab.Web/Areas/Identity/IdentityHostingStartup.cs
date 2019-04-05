using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamCollab.Web.Areas.Identity.Data;
using TeamCollab.Web.Models;

[assembly: HostingStartup(typeof(TeamCollab.Web.Areas.Identity.IdentityHostingStartup))]
namespace TeamCollab.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TeamCollabDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TeamCollabDbContextConnection")));

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<TeamCollabDbContext>();
            });
        }
    }
}