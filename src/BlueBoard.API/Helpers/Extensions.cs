using System.Text;
using System.Threading.Tasks;
using BlueBoard.API.Options;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BlueBoard.API.Helpers
{
    public static class Extensions
    {
        private static string _jwtSectionName = "Jwt";

        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(_jwtSectionName));
            var options = configuration.GetSection(_jwtSectionName).Get<JwtOptions>();
            services.AddAuthentication(i =>
                {
                    i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                        ValidIssuer = options.Issuer,
                        ValidAudience = options.ValidAudience,
                        ValidateAudience = options.ValidateAudience,
                        ValidateLifetime = options.ValidateLifetime
                    };
                });
        }

        public static void RunMigrations(this IApplicationBuilder builder, IMigrationRunner migrationRunner)
        {
            Task.Run(() =>
            {
                if (migrationRunner.HasMigrationsToApplyUp())
                {
                    migrationRunner.MigrateUp();
                }
            });
        }
    }
}
