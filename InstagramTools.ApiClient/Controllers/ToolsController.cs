using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ToolsController(ILogger<ToolsController> logger, IInstaToolsService instaToolsService)
        {
            this._logger = logger;
            this._instaToolsService = instaToolsService;
        }


        [HttpGet]
        [Route("clean-my-following")]
        public async Task CleanMyFollowing()
        {
            await _instaToolsService.BuildApiManagerAsync(new LoginModel()
            {
                Password = KievPassword,
                Username = KievLogin
            });
            await _instaToolsService.CleanMyFollowing(0);
        }


        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            await this._instaToolsService.BuildApiManagerAsync(new LoginModel()
            {
                Password = KievPassword,
                Username = KievLogin
            });
            await this._instaToolsService.CleanMyFollowing(0);
            return new[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
