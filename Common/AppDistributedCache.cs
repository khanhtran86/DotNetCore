using DotnetCoreVCB.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace DotnetCoreVCB.Common
{
    public interface IAppDistributedCache
    {
        public T FromCache<T>(String cacheKey);

        public void SaveToCache(String cacheKey, object obj);
    }
    public class AppDistributedCache: IAppDistributedCache
    {
        IDistributedCache appCache;

        public AppDistributedCache(IDistributedCache _appCache)
        {
            this.appCache = _appCache;
        }

        public T FromCache<T>(String cacheKey)
        {
            byte[] valueFromCache = this.appCache.Get(cacheKey);
            String text2Json = System.Text.Encoding.UTF8.GetString(valueFromCache);
            T returnList = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(text2Json);
            return returnList;
        }


        public void SaveToCache(String cacheKey, object obj)
        {
            String obj2json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] save2Cache = System.Text.Encoding.UTF8.GetBytes(obj2json);

            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));

            this.appCache.Set(cacheKey, save2Cache, options);
        }
    }
}
