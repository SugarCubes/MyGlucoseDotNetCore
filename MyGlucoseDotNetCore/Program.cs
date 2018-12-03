using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services;
using System.IO;

namespace MyGlucoseDotNetCore
{
    public class Program
    {
        public static void Main( string[] args )
        {
            //BuildWebHost(args).Run();                     // ASP.NET CORE 2.0

            var hostUrl = "http://0.0.0.0:51874";

            var host = WebHost.CreateDefaultBuilder( args )
                                .UseKestrel()
                                .UseUrls(hostUrl)   // Override http://localhost:5000 to allow from outside machines
				                .UseContentRoot( Directory.GetCurrentDirectory() )
                                .UseIISIntegration()
                                .UseApplicationInsights()
                                .UseStartup<Startup>()
                                .Build();

            using( var scope = host.Services.CreateScope() )
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                db.Database.Migrate();

                var roleManager = scope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
                new DatabaseSeeder( db, roleManager ).SeedRoles();

            } // using

            host.Run();

        } // Main

        public static IWebHost BuildWebHost( string[] args ) =>
            WebHost.CreateDefaultBuilder( args )
                .UseStartup<Startup>()
                .Build();
    }
}
