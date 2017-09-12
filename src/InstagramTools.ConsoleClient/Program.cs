using System;
using System.IO;
using System.Threading.Tasks;

using AutoMapper;

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
                // create service collection
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                //Configure DataBaseContext
                string connection = GetConnectionStringForMachine(Configuration);
                serviceCollection.AddDbContext<InstagramToolsContext>(options => options.UseSqlServer(connection));

                // Application application = new Application(serviceCollection);
                IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

                // Entry to run app
                var application = serviceProvider.GetService<App>();

                // Run application
                Task.Run(async () => { await application.StartApp(); }).Wait();
                //Task.Run(async () => { await application.StartApp(); }).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}!!!!");
                Console.ReadLine();
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            //Add Logging
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug();

            serviceCollection.AddSingleton(loggerFactory); // Add first my already configured instance
            serviceCollection.AddLogging(); // Allow ILogger<T>

            //Add IConfigurationRoot
            Configuration = GetConfiguration();
            serviceCollection.AddSingleton<IConfigurationRoot>(Configuration);

            //// add services
            serviceCollection.AddTransient<IInstaToolsService, InstaToolsService>();
            serviceCollection.AddTransient<IInstaApiBuilder, InstaApiBuilder>();
            serviceCollection.AddAutoMapper();

            // add app
            serviceCollection.AddTransient<App>();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"Settings/appsettings.dev.json", optional: true, reloadOnChange: true)
                .Build();
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
            return configuration.GetConnectionString("DefaultConnection");
        }
    }


}
