using Application.Common.Interfaces;
using detree.Infrastructure.Services;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(
            //        configuration.GetConnectionString("DB_CONNECTION_STRING"),
            //        b => b.MigrationsAssembly("detree")));

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(
            //        configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DB_CONNECTION_STRING")));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            if (environment.IsEnvironment("Test"))
            {
                services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                    {
                        options.Clients.Add(new Client
                        {
                            ClientId = "detree.IntegrationTests",
                            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                            ClientSecrets = { new Secret("secret".Sha256()) },
                            AllowedScopes = { "detree.detree", "openid", "profile" }
                        });
                    }).AddTestUsers(new List<TestUser>
                    {
                        new TestUser
                        {
                            SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
                            Username = "tim.ilgiz@gmail.com",
                            Password = "89!Dm108",
                            Claims = new List<Claim>
                            {
                                new Claim(JwtClaimTypes.Email, "tim.ilgiz@gmail.com")
                            }
                        }
                    });
            }
            else
            {
                services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

                services.AddTransient<IDateTime, DateTimeService>();
                services.AddTransient<IIdentityService, IdentityService>();
                //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            }

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;


        }
    } 
}
