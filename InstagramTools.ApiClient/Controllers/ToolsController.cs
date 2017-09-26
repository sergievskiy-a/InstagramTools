using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InstagramTools.Common.Helpers;
using InstagramTools.Common.Models;
using InstagramTools.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InstagramTools.ApiClient.Controllers
{
    [Route("api/[controller]")]
    public class ToolsController : Controller
    {
        private const string KievLogin = "bad.kiev";
        private const string KievPassword = "fckdhadiach";

        private readonly IInstaToolsService _instaToolsService;
        private readonly ILogger<ToolsController> _logger;
        private readonly TasksMonitor _monitor;

        public ToolsController(ILogger<ToolsController> logger, IInstaToolsService instaToolsService, TasksMonitor monitor)
        {
            this._logger = logger;
            this._instaToolsService = instaToolsService;
            this._monitor = monitor;
            _monitor.AddTask("LongTask", new CancellationTokenSource());
        }

        //TODO : CHANGE TO POST
        [HttpGet]
        [Route("login")]
        public async Task<OperationResult> Login()
        {
            var result = await _instaToolsService.BuildApiManagerAsync(new LoginModel()
            {
                Password = KievPassword,
                Username = KievLogin
            });
            return result;
        }


        [HttpGet]
        [Route("clean-my-following")]
        public string CleanMyFollowing()
        {
            //var t = Task.Factory.StartNew(()=> _instaToolsService.CleanMyFollowing(cts.Token, 0), cts.Token);
            //cts.Cancel();
            return "Started";
        }

        [HttpGet]
        [Route("cancel")]
        public string Cancel()
        {
            var taskToken = _monitor.GetTokenSource("LongTask");
            if (taskToken == null) return "token is null";
            taskToken.Cancel();
            return "Canceled";
        }


        [HttpGet]
        [Route("start-long-task")]
        public async Task<int> StartLongTask()
        {
            return await LongTask(_monitor.GetTokenSource("LongTask").Token);
        }

        private async Task<int> LongTask(CancellationToken token)
        {
            var counter = 1;         
            for (var i = 0; i < 1000; i++)
            {
                if (token.IsCancellationRequested)
                {
                    _logger.LogDebug("CANCELED");
                    return counter;
                }
                await Task.Delay(2000);
                counter++;
            }
            return counter;
        }
    }
}
