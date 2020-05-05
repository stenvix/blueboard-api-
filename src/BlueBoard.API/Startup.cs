using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using BlueBoard.API.Contracts.Base;
using BlueBoard.API.Filters;
using BlueBoard.API.Helpers;
using BlueBoard.API.Swagger;
using BlueBoard.Mail.Services;
using BlueBoard.Module.Common;
using BlueBoard.Module.Common.Validation;
using BlueBoard.Module.Identity.Commands.SignIn;
using BlueBoard.Module.Identity.Helpers;
using BlueBoard.Module.Mail.Config;
using BlueBoard.Module.Trip.Commands.Create;
using BlueBoard.Persistence;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using BlueBoard.Persistence.Migrations;
using BlueBoard.Persistence.Postgres;
using BlueBoard.Persistence.Repositories;
using Dapper;
using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace BlueBoard.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add(typeof(BlueBoardExceptionFilter)))
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo {Title = "BlueBoard API", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
                config.AddSecurityDefinition(
                    "oauth2",
                    new OpenApiSecurityScheme
                    {
                        Description =
                            "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                config.OperationFilter<SecurityRequirementsOperationFilter>();
                config.DocumentFilter<LowercaseDocumentFilter>();
            });

            //Libs
            services.AddMediatR(typeof(SignInCommandHandler), typeof(CreateTripCommandHandler));
            services.AddValidatorsFromAssemblyContaining<SignInCommandHandler>();
            services.AddValidatorsFromAssemblyContaining<CreateTripCommandHandler>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(typeof(ApiRequest));
            services.AddMemoryCache();
            services.AddFluentMigratorCore().ConfigureRunner(builder =>
                    builder.AddPostgres()
                        .WithMigrationsIn(typeof(UnitOfWork).Assembly)
                        .WithGlobalConnectionString(this.Configuration.GetConnectionString("Default")))
                .AddLogging(i => i.AddFluentMigratorConsole());
            services.AddSingleton<IVersionTableMetaData, VersionTable>();
            services.AddJwt(this.Configuration);
            services.AddHttpContextAccessor();
            if (this.IsDevelopmentEnvironment())
            {
                services.AddCors();
            }

            //Options
            services.Configure<MailOptions>(this.Configuration.GetSection("Mail"));

            //Services
            services.AddSingleton<IAccessHandler, AccessHandler>();
            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<IConnectionStringProvider>(
                new ConnectionStringProvider("Default", this.Configuration));
            services.AddSingleton<IConnectionFactory, PostgresConnectionFactory>();
            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITripRepository, TripRepository>();
            services.AddSingleton<IParticipantRepository, ParticipantRepository>();

            services.AddTransient<ICurrentUserProvider, CurrentUserProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IMigrationRunner migrationRunner)
        {
            if (this.IsDevelopmentEnvironment())
            {
                app.UseDeveloperExceptionPage();
                app.RunMigrations(migrationRunner);
                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "BlueBoard API v1");
                config.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            this.SetupDapper();
        }

        private void SetupDapper()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        private bool IsDevelopmentEnvironment()
        {
            return this.Environment.IsDevelopment() || this.Environment.IsEnvironment("Docker") ||
                   this.Environment.IsEnvironment("Heroku");
        }
    }
}
