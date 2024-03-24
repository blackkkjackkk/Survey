using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Survey.Data;
using System;
using System.Threading.Tasks;

namespace Survey
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    if (context.Database.IsSqlServer())
                    {
                        await context.Database.MigrateAsync();
                    }

                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await ApplicationDbContextSeed.SeedRoleAndClaimAsync(userManager, roleManager);
                    await ApplicationDbContextSeed.SeedDefaultUsersAsync(userManager, roleManager);
                    await ApplicationDbContextSeed.SeedQuestionTypeDataAsync(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Application {typeof(Startup).Assembly.GetName().Name} start-up failed error: {ex.Message}");
                }
             }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
