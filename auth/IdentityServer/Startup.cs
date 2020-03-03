using System;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net;
using Application.Extensions;
using IdentityServer.Configuration;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Infrastructure.Persistence;

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

        protected virtual void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(ConfigureDatabase);
        }

        protected virtual void ConfigureDatabase(DbContextOptionsBuilder ctxBuilder)
        {
            ctxBuilder.UseNpgsql(
                Configuration.GetConnectionString("DB_CONNECTION_STRING"),
                b => b.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.FullName));

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration, Environment);
            services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>();

            var builder = services.AddIdentityServer()
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = ConfigureDatabase;
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<AppUser>();

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddTransient<IProfileService, IdentityClaimsProfileService>();
            services.AddMvc(options => { options.EnableEndpointRouting = false; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(builder =>
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                    }
                }));

            var serilog = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.File(@"authserver_log.txt");

            loggerFactory.WithFilter(new FilterLoggerSettings
            {
                { "IdentityServer4", LogLevel.Debug },
                { "Microsoft", LogLevel.Warning },
                { "System", LogLevel.Warning },
            }).AddSerilog(serilog.CreateLogger());

            app.UseStaticFiles();

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            //app.UseHttpsRedirection();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}