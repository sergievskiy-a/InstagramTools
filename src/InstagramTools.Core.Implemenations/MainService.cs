using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramTools.Api.API.Builder;
using InstagramTools.Common;
using InstagramTools.Core.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InstagramTools.Core
{
    public class MainService<TService> : BaseService
    {
        public readonly ILogger<TService> Logger;
        private readonly IMemoryCache _memoryCache;
        protected IInstaApiBuilder _apiBuilder;
        protected InstagramToolsConfigurations Configurator;

        public MainService(IConfigurationRoot root, ILogger<TService> logger, IMemoryCache memoryCache, IInstaApiBuilder apiBuilder)
        {
            Logger = logger;
            _memoryCache = memoryCache;
            _apiBuilder = apiBuilder;
            Configurator = new InstagramToolsConfigurations(root);
        }

        public override void HandleError(Exception ex)
        {
            Logger.LogError(ex.ToString());
        }

        public override TModel GetFromCache<TModel>(string key)
        {
            return _memoryCache.Get<TModel>(key);
        }

        public override void AddToCache<TModel>(string key, TimeSpan expiration, TModel model)
        {
            _memoryCache.Set(key, model, expiration);
        }
    }
}
