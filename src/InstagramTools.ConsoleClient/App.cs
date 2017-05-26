using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramTools.Common.Interfaces;
using InstagramTools.Common.Models;
using Microsoft.Extensions.Logging;

namespace InstagramTools.ConsoleClient
{
    public class App
    {
        private const string KievLogin = "bad.kiev";
        private const string KievPassword = "fckdhadiach";

        private readonly IInstaToolsService _instaToolsService;
        private readonly ILogger<App> _logger;

        public App(IInstaToolsService instaToolsService, ILogger<App> logger)
        {
            _instaToolsService = instaToolsService;
            _logger = logger;
        }


        //Main Code//
        public async void StartApp()
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
            
            var testUsername = "kotsemir.nazariy";
            var followUsersWhichLikeLastPostResult = await _instaToolsService.FollowUsersWhichLikeLastPostAsync(testUsername, 0);
            
        }
    }
}
