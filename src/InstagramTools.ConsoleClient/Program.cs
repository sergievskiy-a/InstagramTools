using System;
using System.Text;
using InstagramTools.Api.API;
using InstagramTools.Api.API.Builder;
using InstagramTools.Api.Common.Models;
using InstagramTools.Common;
using InstagramTools.Common.Interfaces;
using InstagramTools.Common.Models;
using InstagramTools.ConsoleClient.Samples;
using InstagramTools.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InstagramTools.ConsoleClient
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // create service collection
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                // create service provider
                var serviceProvider = serviceCollection.BuildServiceProvider();

                // entry to run app
                var application = serviceProvider.GetService<App>();

                // run application
                application.StartApp();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}!!!!");
                Console.ReadLine();
                return;
            }

        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
                // add logging
                serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());
                serviceCollection.AddLogging();

                // add services
                serviceCollection.AddTransient<IInstaToolsService, InstaToolsService>();
                serviceCollection.AddTransient<IInstaApiBuilder, InstaApiBuilder>();

                // add app
                serviceCollection.AddTransient<App>();
        }
    }


}
