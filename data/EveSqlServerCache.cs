using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using eZet.EveLib.Core.Cache;
using Microsoft.Data.Entity;
using Serilog;

using ts.domain;

namespace ts.data
{
    public class EveSqlServerCache : IEveLibCache
    {
        private readonly ILogger _logger;
        private readonly AccountContextProvider _accountContextProvider;
        private static int _counter = 0;

        public EveSqlServerCache(ILogger logger, AccountContextProvider accountContextProvider)
        {
            _logger = logger;
            _accountContextProvider = accountContextProvider;

        }

        private string UriToKey(Uri uri)
        {
            return uri.ToString();
        }
        public async Task StoreAsync(Uri uri, DateTime cacheTime, string data)
        {
            string key = UriToKey(uri);
            _logger.Debug("{method} {cacheTime} {key} {length}", "EveSqlServerCache::StoreAsync", cacheTime, key, data.Length);

            ++_counter;

            using (var ctx = _accountContextProvider.Context)
            {
                if (_counter % 100 == 0)
                {
                    var todel = await ctx.CacheEntries.Where(x => x.CachedUntil < DateTime.UtcNow).ToListAsync();
                    ctx.CacheEntries.RemoveRange(todel);
                }

                // Add new entry
                var entry = await ctx.CacheEntries.SingleOrDefaultAsync(c => c.Key == key);
                if (entry == null)
                {
                    entry = new CacheEntry()
                    {
                        CachedUntil = cacheTime,
                        Data = data,
                        Key = key
                    };
                    ctx.CacheEntries.Add(entry);
                }
                else
                {
                    entry.CachedUntil = cacheTime;
                    entry.Data = data;
                }

                await ctx.SaveChangesAsync();
            }
        }

        public async Task<string> LoadAsync(Uri uri)
        {
            string key = UriToKey(uri);
            _logger.Debug("{method} {key}", "EveSqlServerCache::LoadAsync", key);

            using (var ctx = _accountContextProvider.Context)
            {
                var entry = await ctx.CacheEntries.SingleOrDefaultAsync(c => c.Key == key);
                if (entry == null || entry.CachedUntil < DateTime.UtcNow)
                    return null;
                return entry.Data;
            }
        }

        public async Task<Tuple<bool, DateTime>> TryGetExpirationDateAsync(Uri uri)
        {
            string key = UriToKey(uri);
            _logger.Debug("{method} {key}", "EveSqlServerCache::TryGetExpirationDateAsync", key);

            using (var ctx = _accountContextProvider.Context)
            {
                var entry = await ctx.CacheEntries.SingleOrDefaultAsync(c => c.Key == key);
                if (entry == null)
                    return new Tuple<bool, DateTime>(false, new DateTime());
                else
                {
                    return new Tuple<bool, DateTime>(true, entry.CachedUntil.ToUniversalTime());
                }
            }
        }

        public bool TryGetExpirationDate(Uri uri, out DateTime value)
        {
            string key = UriToKey(uri);
            _logger.Debug("{method} {key}", "EveSqlServerCache::TryGetExpirationDate", key);

            var t = TryGetExpirationDateAsync(uri);
            t.Wait();

            value = t.Result.Item2;
            return t.Result.Item1;
        }
    }
}
