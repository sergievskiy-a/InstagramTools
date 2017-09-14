using System;
using System.Threading.Tasks;
using InstagramTools.Common.Models;
using InstagramTools.Core.Interfaces;
using InstagramTools.Data;

using Microsoft.Extensions.Logging;

namespace InstagramTools.ConsoleClient
{
    public class App
    {
        private const string KievLogin = "bad.kiev";
        private const string KievPassword = "fckdhadiach";

        private readonly IInstaToolsService instaToolsService;
        private readonly ILogger<App> logger;

        public App(ILogger<App> logger, IInstaToolsService instaToolsService, InstagramToolsContext context)
        {
            // Create DB, if not exist
            context.Database.EnsureCreated();

            this.logger = logger;
            this.instaToolsService = instaToolsService;
        }


        // Main Code//
        public async Task<int> StartApp()
        {
            this.logger.LogInformation("Application started! :)");

            var kievLoginModel = new LoginModel() { Username = KievLogin, Password = KievPassword };

            var buildApiManagerResult = await this.instaToolsService.BuildApiManagerAsync(kievLoginModel);
            if (!buildApiManagerResult.Success)
            {
                throw new Exception(buildApiManagerResult.Message);
            }

            // var kievphoto = "kievphoto";
            // var kievblog = "kievblog";
            // var kpi_live = "kpi_live";
            // var kievgram = "kievgram";

            // var followingResult = await _instaToolsService.FollowSubscribersOfUser(kievblog);
            var test = await this.instaToolsService.WriteToDbCurrentUserFollowers(10);

            // var followUsersWhichLikeLastPostResult = await _instaToolsService.FollowUsersWhichLikeLastPostAsync(kpi_live);
            Console.WriteLine($"Done!\nSuccess: {test.Success}");

            if (!string.IsNullOrWhiteSpace(test.Message))
            {
                Console.WriteLine($"Message: {test.Message}");
            }

            Console.ReadKey();
            return 1;
        }
    }
}
