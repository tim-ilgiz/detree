using System.Collections.Generic;
using Application;
using Application.Common.Exceptions;
using Application.Common.Grant;
using Application.Common.Interfaces;
using FluentValidation.AspNetCore;
using IdentityServer4.Models;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration, Environment);
            services.AddHttpContextAccessor();
            services.AddHealthChecks()
                .AddDbContextCheck<AppDbContext>();
            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IAppDbContext>())
                .AddNewtonsoftJson();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddInfrastructure(Configuration, Environment);
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(new List<ApiResource>
                {
                    new ApiResource("api.sample", "Sample API")
                })
                .AddInMemoryClients(new List<Client>
                {
                    new Client
                    {
                        ClientId = "Authentication",
                        ClientSecrets =
                        {
                            new Secret("clientsecret".Sha256())
                        },
                        AllowedGrantTypes = {"authentication"},
                        AllowedScopes =
                        {
                            "api.sample"
                        },
                        AllowOfflineAccess = true
                    }
                })
                .AddExtensionGrantValidator<AuthenticationGrant>();

            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddMvc()
                .AddControllersAsServices();
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCustomExceptionHandler();
            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();
            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}