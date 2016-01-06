using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZet.EveLib.Core.Cache;
using eZet.EveLib.EveXmlModule;
using eZet.EveLib.EveXmlModule.Models.Misc;

namespace ts.data 
{
    public class RefTypeDict : IRefTypeDict
    {
        private ICacheLocalProvider _cacheProvider;

        public RefTypeDict(ICacheLocalProvider cacheProvider, IEveLibCache eveLibCache)
        {
            _cacheProvider = cacheProvider;

            EveApi.InitializeCache(eveLibCache);
        }

        private Dictionary<long, string> _firstLevelCache = new Dictionary<long, string>();

        public async Task<string> GetById(long id)
        {
            if (_firstLevelCache.ContainsKey(id))
                return _firstLevelCache[id];

            string key = string.Format("/dict/reftype/{0}", id);

            var value = await _cacheProvider.Get(key, () => GetRefTypeName(id));
            _firstLevelCache[id] = value;

            return value;
        }

        private async Task<Tuple<DateTime, string>> GetRefTypeName(long id)
        {
            if (_referenceType == null)
            {
                var t = new Eve().GetReferenceTypesAsync();
                var result = await t.ConfigureAwait(false);
                _referenceType = result.Result.RefTypes.ToList();
            }

            var refType = _referenceType.FirstOrDefault(x => x.RefTypeId == id);
            if (refType == null)
                return new Tuple<DateTime, string>(DateTime.UtcNow.AddYears(100), "<unknown>");
            else
                return new Tuple<DateTime, string>(DateTime.UtcNow.AddYears(100), refType.RefTypeName);
        }

        private List<ReferenceTypes.ReferenceType> _referenceType;
    }
}
