using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using BlueBoard.API.Contracts.Base;
using BlueBoard.API.Filters;
using BlueBoard.API.Swagger;
using BlueBoard.Common;
using BlueBoard.Mail.Services;
using BlueBoard.Module.Identity.SignIn;
using BlueBoard.Module.Mail.Config;
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
                        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                config.OperationFilter<SecurityRequirementsOperationFilter>();
                config.DocumentFilter<LowercaseDocumentFilter>();
            });
            services.AddMediatR(typeof(SignInCommandHandler));
            services.AddValidatorsFromAssemblyContaining<SignInCommandHandler>();
            services.AddAutoMapper(typeof(ApiRequest));
            services.AddMemoryCache();

            services.AddSingleton<IMailService, MailService>();
            services.Configure<MailOptions>(this.Configuration.GetSection("Mail"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
        }
    }
}
