using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramTools.Api.API.Builder;
using InstagramTools.Common.Interfaces;
using InstagramTools.Common.Models;
using InstagramTools.Core;
using Microsoft.Extensions.Logging;

namespace InstagramTools.ConsoleClient
{
    public class App
    {
        private const string KievLogin = "bad.kiev";
        private const string KievPassword = "fckdhadiach";

        private readonly IInstaToolsService _instaToolsService;
        private readonly ILogger<App> _logger;

        public App(ILogger<App> logger, IInstaToolsService instaToolsService)
        {
            
            _logger = logger;
            _instaToolsService = instaToolsService;
        }


        //Main Code//
        public async Task<int> StartApp()
        {
            _logger.LogInformation("Application started! :)");

            var kievLoginModel = new LoginModel()
            {
                Username = KievLogin,
                Password = KievPassword
            };

            var buildApiManagerResult = await _instaToolsService.BuildApiManagerAsync(kievLoginModel);
            if (!buildApiManagerResult.Success)
            {
                throw new Exception(buildApiManagerResult.Message);
            }
            
            var kievphoto = "kievphoto";
            var kievblog = "kievblog";
            var kpi_live = "kpi_live";
            var kievgram = "kievgram";

            var followingResult = await _instaToolsService.FollowSubscribersOfUser(kievphoto);
            //var followUsersWhichLikeLastPostResult = await _instaToolsService.FollowUsersWhichLikeLastPostAsync(kpi_live);
            Console.WriteLine("Done!");
            Console.ReadKey();
            return 1;
        }
    }
}
