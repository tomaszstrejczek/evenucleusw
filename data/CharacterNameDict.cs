using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZet.EveLib.Core.Cache;
using eZet.EveLib.EveXmlModule;

namespace ts.data
{
    public class CharacterNameDict : ICharacterNameDict
    {
        private ICacheLocalProvider _cacheProvider;
        public CharacterNameDict(ICacheLocalProvider cacheProvider, IEveLibCache eveLibCache)
        {
            _cacheProvider = cacheProvider;

            EveApi.InitializeCache(eveLibCache);
        }

        public async Task<string> GetById(long id)
        {
            string key = string.Format("/dict/CharacterName/{0}", id);

            return await _cacheProvider.Get(key, () => GetCharacterName(id));
        }

        private async Task<Tuple<DateTime, string>> GetCharacterName(long id)
        {
            var result = await new Eve().GetCharacterNameAsync(id);
            return new Tuple<DateTime, string>(DateTime.UtcNow.AddYears(100), result.Result.Characters[0].CharacterName);
        }
    }
}
