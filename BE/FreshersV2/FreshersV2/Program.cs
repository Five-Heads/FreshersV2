using FreshersV2.Data;
using FreshersV2.Data.Models;
using FreshersV2.Hubs;
using FreshersV2.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;

namespace FreshersV2
{

    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            IServiceCollection services = builder.Services;
            var appSettings = services.GetApplicationSettings(builder.Configuration);

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            });

            services
                .AddJwtAuthentication(appSettings)
                .AddApplicationServices()
                .AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200")
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                });

            services
                .AddIdentity<User, IdentityRole>()
                .AddUserManager<UserManager<User>>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddHangfire(configuration =>
                configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(
                        builder.Configuration.GetConnectionString("HangfireConnection")
                    )
            ).AddHangfireServer();

            services.AddDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));
            services.AddSignalR();
            services.AddControllers();
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();

            var app = builder.Build();

            app.UseHangfireDashboard();

            // Configure the HTTP request pipeline.
            app.UseRouting()
                .UseCors("CorsPolicy")
                .UseHttpsRedirection()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<TreasureHuntHub>("/hubs/TreasureHunt");
                    endpoints.MapHub<VoteImageHub>("/hubs/VoteImage");
                    endpoints.MapControllers();
                    endpoints.MapHangfireDashboard();
                });

            app.Run();
        }
    }
}
