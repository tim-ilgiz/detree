using System.Collections.Generic;
using Application.Common.Grant;
using Application.Common.Interfaces;
using IdentityServer4.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("default");
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}