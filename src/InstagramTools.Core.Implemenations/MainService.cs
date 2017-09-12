using System;
using AutoMapper;

using InstagramTools.Common;
using InstagramTools.Core.Implemenations.Configurations;
using InstagramTools.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InstagramTools.Core.Implemenations
{
    public class MainService<TService> : BaseService
    {
        public readonly ILogger<TService> Logger;
        private readonly IMemoryCache memoryCache;
        protected readonly IMapper Mapper;
        protected readonly InstagramToolsContext Context;
        protected InstagramToolsConfigurations Configurator;

        public MainService(IConfigurationRoot root, ILogger<TService> logger, IMemoryCache memoryCache, IMapper mapper, InstagramToolsContext context)
        {
            this.Logger = logger;
            this.memoryCache = memoryCache;
            this.Mapper = mapper;
            this.Context = context;
            this.Configurator = new InstagramToolsConfigurations(root);
        }

        public override void HandleError(Exception ex)
        {
            this.Logger.LogError(ex.ToString());
        }

        public override TModel GetFromCache<TModel>(string key)
        {
            return this.memoryCache.Get<TModel>(key);
        }

        public override void AddToCache<TModel>(string key, TimeSpan expiration, TModel model)
        {
            this.memoryCache.Set(key, model, expiration);
        }
    }
}
