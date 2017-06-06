﻿using System;
using System.IO;
using System.Threading.Tasks;
using InstagramTools.Api.API.Builder;
using InstagramTools.Core.Implemenations;
using InstagramTools.Core.Interfaces;
using InstagramTools.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InstagramTools.ConsoleClient
{
    public class Program
    {
        public static IConfigurationRoot Configuration;

        private static void Main(string[] args)
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory).Replace(@"bin\Debug\netcoreapp1.1", "");
                var builder = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("Settings/appsettings.dev.json", optional: false, reloadOnChange: true);

                Configuration = builder.Build();

                // create service collection
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                //Configure DataBaseContext
                string connection = GetConnectionStringForMachine(Configuration);
                serviceCollection.AddDbContext<InstagramToolsContext>(options => options.UseSqlServer(connection));


                // create service provider
                var serviceProvider = serviceCollection.BuildServiceProvider();

                // entry to run app
                var application = serviceProvider.GetService<App>();

                // run application
                Task.Run(async () => { await application.StartApp(); }).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}!!!!");
                Console.ReadLine();
            }

        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
                // add logging
                serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());
                serviceCollection.AddLogging();

            //// add services
            serviceCollection.AddTransient<IInstaToolsService, InstaToolsService>();
            serviceCollection.AddTransient<IInstaApiBuilder, InstaApiBuilder>();

            // add app
            serviceCollection.AddTransient<App>();
        }

        //Return DbConnString based on Machine's name
        private static string GetConnectionStringForMachine(IConfigurationRoot configuration)
        {
            if (Environment.MachineName.Equals("SERGIEVSKIY-DEV", StringComparison.OrdinalIgnoreCase))
            {
                return configuration.GetConnectionString("AzureTestDB");
            }
            if (Environment.MachineName.Equals("SERGIEVSKIY-LENOVO", StringComparison.OrdinalIgnoreCase))
            {
                return configuration.GetConnectionString("lenovo-local");
            }
            return configuration.GetConnectionString("AzureConnection");
        }
    }


}
