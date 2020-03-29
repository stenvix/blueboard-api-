using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using BlueBoard.API.Contracts.Base;
using BlueBoard.API.Filters;
using BlueBoard.API.Helpers;
using BlueBoard.API.Swagger;
using BlueBoard.Mail.Services;
using BlueBoard.Module.Identity.Helpers;
using BlueBoard.Module.Identity.SignIn;
using BlueBoard.Module.Mail.Config;
using BlueBoard.Persistence;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using BlueBoard.Persistence.Postgres;
using BlueBoard.Persistence.Repositories;
using Dapper;
using FluentMigrator.Runner;
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
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
            services.AddMediatR(typeof(SignInCommandHandler));
            services.AddValidatorsFromAssemblyContaining<SignInCommandHandler>();
            services.AddAutoMapper(typeof(ApiRequest));
            services.AddMemoryCache();
            services.AddFluentMigratorCore().ConfigureRunner(builder =>
                    builder.AddPostgres()
                        .WithMigrationsIn(typeof(UnitOfWork).Assembly)
                        .WithGlobalConnectionString(this.Configuration.GetConnectionString("Default")))
                .AddLogging(i => i.AddFluentMigratorConsole());
            services.AddJwt(this.Configuration);

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "Docker")
            {
                app.UseDeveloperExceptionPage();
                app.RunMigrations(migrationRunner);
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
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            this.SetupDapper();
        }

        private void SetupDapper()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
    }
}
