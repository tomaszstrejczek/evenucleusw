using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZet.EveLib.Core.Cache;
using Newtonsoft.Json;

namespace ts.api
{
    public class CacheEveLibProvider : ICacheLocalProvider
    {
        private readonly IEveLibCache _eveLibCache;

        public CacheEveLibProvider(IEveLibCache eveLibCache)
        {
            _eveLibCache = eveLibCache;
        }

        public async Task<T> Get<T>(string key, Func<Task<Tuple<DateTime, T>>> provider) where T : class
        {
            if (provider == null)
                throw new ArgumentNullException();

            var uri = new Uri("file://cache/" + key);

            DateTime dt;
            var found = _eveLibCache.TryGetExpirationDate(uri, out dt);
            if (found && dt >= DateTime.UtcNow)
            {
                var cached = await _eveLibCache.LoadAsync(uri).ConfigureAwait(false);
                if (cached != null)
                {
                    var result = JsonConvert.DeserializeObject<T>(cached);
                    if (result != null)
                        return result;
                }
            }

            // not found, or expired, or unable to deserialize
            var f = await provider().ConfigureAwait(false);
            var data = JsonConvert.SerializeObject(f.Item2);
            await _eveLibCache.StoreAsync(uri, f.Item1, data).ConfigureAwait(false);
            return (T)f.Item2;
        }
    }
}
