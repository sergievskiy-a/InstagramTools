using System;
using AutoMapper;
using InstagramTools.Api.API.Builder;
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
        private readonly IMemoryCache _memoryCache;
        protected readonly IMapper _mapper;
        protected readonly InstagramToolsContext _context;
        protected InstagramToolsConfigurations Configurator;

        public MainService(IConfigurationRoot root, ILogger<TService> logger, IMemoryCache memoryCache, IMapper mapper, InstagramToolsContext context)
        {
            Logger = logger;
            _memoryCache = memoryCache;
            _mapper = mapper;
            _context = context;
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
