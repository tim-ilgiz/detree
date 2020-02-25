using System.Reflection;
using Application.Common.Behaviours;
using Application.Interfaces.UseCases;
using Application.UseCases;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IRegisterUserUseCase), typeof(RegisterUserUseCase));
            services.AddTransient(typeof(ILoginUseCase), typeof(LoginUseCase));
            services.AddTransient(typeof(IExchangeRefreshTokenUseCase), typeof(ExchangeRefreshTokenUseCase));

            return services;
        }
    }
}