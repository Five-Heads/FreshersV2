using FreshersV2.Data;
using FreshersV2.Services.BaseImage;
using FreshersV2.Services.BlurredImage;
using FreshersV2.Services.Group;
using FreshersV2.Services.Identity;
using FreshersV2.Services.TreasureHunt;
using FreshersV2.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FreshersV2.Jobs;
using FreshersV2.Services.ImageVote;
using FreshersV2.Services.Leaderboard;
using FreshersV2.Services.BaseImageContest;

namespace FreshersV2.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IGroupService, GroupService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<ITreasureHuntService, TreasureHuntService>()
                .AddTransient<IBaseImageService, BaseImageService>()
                .AddTransient<IBlurredImageService, BlurredImageService>()
                .AddTransient<IImageVoteService,ImageVoteService>()
                .AddTransient<VoteRoundJob,VoteRoundJob>()
                .AddTransient<ILeaderboardService,LeaderboardService>()
                .AddTransient<IBlurredImageContestService, BlurredImageContestService>();

        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
            => services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString,
                        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                    ), ServiceLifetime.Transient);

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            ApplicationSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static ApplicationSettings GetApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<ApplicationSettings>(applicationSettingsConfiguration);
            // configure jwt authentication
            var appSettings = applicationSettingsConfiguration.Get<ApplicationSettings>();

            return appSettings;
        }
    }
}