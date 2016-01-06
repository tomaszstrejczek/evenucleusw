using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using eZet.EveLib.Core.Cache;
using eZet.EveLib.EveXmlModule;
using eZet.EveLib.EveXmlModule.Models.Misc;
using Microsoft.Data.Entity;
using Serilog;

using ts.domain;
using ts.staticcontent;

namespace ts.data
{
    public class TypeNameDict : ITypeNameDict
    {
        private readonly AccountContextProvider _accountContextProvider;
        private readonly ILogger _logger;

        public TypeNameDict(AccountContextProvider accountContextProvider, IEveLibCache eveLibCache, ILogger logger)
        {
            _accountContextProvider = accountContextProvider;
            _logger = logger;

            EveApi.InitializeCache(eveLibCache);
        }

        public async Task<List<Tuple<long, string>>> GetById(IEnumerable<long> ids)
        {
            _logger.Debug("{method} ids count: {count}", "TypeNameDict::GetById", ids.Count());

            return ids.Select(x => new Tuple<long, string>(x, TypeFromYaml.FromTypeId(x))).ToList();

            if (ids.Count() == 0)
                return new List<Tuple<long, string>>();

            using (var ctx = _accountContextProvider.Context)
            {
                // Delete old entries to clear space
                var todel = await ctx.TypeNameEntries.Where(t => t.CachedUntil < DateTime.UtcNow).ToListAsync();
                ctx.TypeNameEntries.RemoveRange(todel);

                // hit entries
                var hit = await ctx.TypeNameEntries.Where(x => ids.Contains(x.Key)).ToListAsync();

                // missing items
                var missingids = ids.Except(hit.Select(x => x.Key)).ToList();

                if (missingids.Count == 0)
                    return hit.Select(x => new Tuple<long, string>(x.Key, x.Data)).ToList();

                // query for missing items
                // the app limit query up to 250 ids - so we query in the loop
                const int LIMIT = 250;
                var missingentries = new List<TypeName.TypeData>();
                while (missingids.Count > 0)
                {
                    int totake = Math.Min(missingids.Count, LIMIT);
                    var toask = missingids.GetRange(0, totake);
                    var types = await new Eve().GetTypeNameAsync(toask.ToArray());
                    missingids.RemoveRange(0, totake);
                    missingentries.AddRange(types.Result.Types);
                }

                // store missing entries
                var tosave = missingentries.Select(x => new TypeNameEntry() { Key = x.TypeId, CachedUntil = DateTime.UtcNow.AddMonths(1), Data = x.TypeName });
                ctx.TypeNameEntries.AddRange(tosave);
                await ctx.SaveChangesAsync();

                var result1 = hit.Select(x => new Tuple<long, string>(x.Key, x.Data));
                var result2 = tosave.Select(x => new Tuple<long, string>(x.Key, x.Data));

                var result = result1.Union(result2).ToList();

                return result;
            }
        }
    }
}
