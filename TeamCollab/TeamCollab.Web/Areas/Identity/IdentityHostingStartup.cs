//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using TeamCollab.Data;
//using TeamCollab.Data.Models;

//[assembly: HostingStartup(typeof(TeamCollab.Web.Areas.Identity.IdentityHostingStartup))]
//namespace TeamCollab.Web.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) => {
//                services.AddDbContext<TeamCollabDbContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("TeamCollabDbContextConnection")));

//                services.AddDefaultIdentity<User>()
//                    .AddEntityFrameworkStores<TeamCollabDbContext>();
//            });
//        }
//    }
//}