using System;
using System.Threading.Tasks;
using InstagramTools.Common.Helpers;

namespace InstagramTools.Common
{
    public abstract class BaseService
    {
        public async Task<OperationResult> ProcessRequestAsync(Func<Task<OperationResult>> func)
        {
            try
            {
                var response = await func();
                return response;
            }
            catch (Exception ex)
            {
                this.HandleError(ex);
                return new OperationResult(false, ex.Message);
            }
        }

        public async Task<OperationResult<TModel>> ProcessRequestAsync<TModel>(Func<Task<OperationResult<TModel>>> func)
        {
            try
            {
                var response = await func();
                return response;
            }
            catch (Exception ex)
            {
                this.HandleError(ex);
                return new OperationResult<TModel>(false, ex.Message);
            }
        }

        public async Task<OperationResult<TModel>> ProcessRequestUseCacheAsync<TModel>(Func<Task<OperationResult<TModel>>> func, string key, TimeSpan expiration)
        {
            try
            {
                var valueFromCahce = this.GetFromCache<TModel>(key);
                if (valueFromCahce != null)
                {
                    return new OperationResult<TModel>(valueFromCahce);
                }

                var response = await func();
                if (response.Success)
                {
                    this.AddToCache(key, expiration, response.Model);
                }

                return response;
            }
            catch (Exception ex)
            {
                this.HandleError(ex);
                return new OperationResult<TModel>(false, ex.Message);
            }
        }

        public async Task<TModel> ExecuteUseCacheAsync<TModel>(Func<Task<TModel>> func, string key, TimeSpan expiration)
        {
            var valueFromCahce = this.GetFromCache<TModel>(key);
            if (valueFromCahce != null)
            {
                return valueFromCahce;
            }

            return await func();
        }

        public abstract void HandleError(Exception ex);
        public abstract TModel GetFromCache<TModel>(string key);
        public abstract void AddToCache<TModel>(string key, TimeSpan expiration, TModel model);
    }
}
