using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public class Program
    {
        private const string ApplicationEnvironmentVariablesPrefix = "AS_";
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (context, builder) => builder.AddEnvironmentVariables(ApplicationEnvironmentVariablesPrefix))
                .UseStartup<Startup>();
    }
}