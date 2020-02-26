using System.Linq;
using System.Reflection;
using Application.Interfaces.UseCases;
using Application.UseCases;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {   

            
            //services.AddTransient(typeof(IRegisterUserUseCase), typeof(RegisterUserUseCase));
//            services.AddTransient(typeof(ILoginUseCase), typeof(LoginUseCase));
//            services.AddTransient(typeof(IExchangeRefreshTokenUseCase), typeof(ExchangeRefreshTokenUseCase));

            return services;
        }

        public static void AddScopedFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var allServices = assembly.GetTypes().Where(p =>
                p.GetTypeInfo().IsClass &&
                !p.GetTypeInfo().IsAbstract);
            foreach (var type in allServices)
            {
                var allInterfaces = type.GetInterfaces();
                var mainInterfaces = allInterfaces.Except
                    (allInterfaces.SelectMany(t => t.GetInterfaces()));
                foreach (var itype in mainInterfaces)
                {
                    services.AddScoped(itype, type); // if you want you can pass lifetime as a parameter
                }
            }
        }
    }
}