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
        private static IInstaToolsService _instaToolsService;

        private const string kievLogin = "bad.kiev";
        private const string kievPassword = "fckdhadiach";

        private static OperationResult ConfigureServices()
        {
            try
            {
                //setup our DI
                var serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .AddSingleton<IInstaToolsService, InstaToolsService>()
                    .BuildServiceProvider();

                //configure console logging
                serviceProvider
                    .GetService<ILoggerFactory>()
                    .AddConsole(LogLevel.Debug);

                var logger = serviceProvider.GetService<ILoggerFactory>()
                    .CreateLogger<Program>();
                logger.LogDebug("Starting application");
                _instaToolsService = serviceProvider.GetService<IInstaToolsService>();
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message);
            }

        }

        private static void Main(string[] args)
        {
            var configResult = ConfigureServices();
            if (!configResult.Success)
            {
                Console.WriteLine($"ERROR: {configResult.Message}!!!!");
                Console.ReadLine();
                return;
            }

            var kievLoginModel = new LoginModel()
            {
                Username = kievLogin,
                Password = kievPassword
            };
            
            _instaToolsService.BuildApiManager(kievLoginModel);

            var testUsername = "kotsemir.nazariy";
            _instaToolsService.FollowUsersWhichLikeLastPost(testUsername, 0);
            
        }
    }


}
