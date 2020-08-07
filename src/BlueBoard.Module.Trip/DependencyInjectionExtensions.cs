using AutoMapper;
using BlueBoard.Module.Trip.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BlueBoard.Module.Trip
{
    public static class DependencyInjectionExtensions
    {
        public static void AddTripModule(this IServiceCollection services)
        {
            services.AddTransient<ITripRepository, TripRepository>();

            services.AddAutoMapper(typeof(DependencyInjectionExtensions).Assembly);
            services.AddMediatR(typeof(DependencyInjectionExtensions).Assembly);
            services.AddValidatorsFromAssembly(typeof(DependencyInjectionExtensions).Assembly);
        }
    }
}
