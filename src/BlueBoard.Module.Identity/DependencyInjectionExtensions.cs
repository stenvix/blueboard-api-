using AutoMapper;
using BlueBoard.Module.Identity.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BlueBoard.Module.Identity
{
    public static class DependencyInjectionExtensions
    {
        public static void AddIdentityModule(this IServiceCollection services)
        {
            services.AddTransient<IParticipantRepository, ParticipantRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(DependencyInjectionExtensions).Assembly);
            services.AddMediatR(typeof(DependencyInjectionExtensions).Assembly);
            services.AddValidatorsFromAssembly(typeof(DependencyInjectionExtensions).Assembly);
        }
    }
}
