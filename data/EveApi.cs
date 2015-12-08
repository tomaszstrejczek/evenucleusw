using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Serilog;

using eZet.EveLib.Core.Cache;
using eZet.EveLib.Core.RequestHandlers;
using eZet.EveLib.EveXmlModule;
using eZet.EveLib.EveXmlModule.Models.Character;
using eZet.EveLib.EveXmlModule.Models.Misc;

using ts.domain;

namespace ts.data
{
    public class EveApi : IEveApi
    {
        private readonly ILogger _logger;

        public static void InitializeCache(IEveLibCache eveCache)
        {
            //if (eZet.EveLib.Core.Config.CacheFactory == null)
            eZet.EveLib.Core.Config.CacheFactory = (arg) => eveCache;
        }
        public EveApi(ILogger logger, IEveLibCache eveCache)
        {
            _logger = logger;

            InitializeCache(eveCache);
        }


        public List<Character> GetCharacters(long keyid, string vcode)
        {
            _logger.Debug("{method} {keyid}", "GetCharacters", keyid);

            try
            {
                var key = new CharacterKey(keyid, vcode);
                var chars = key.Characters;
                return chars.ToList();
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetCharacters", e.ToString());

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }


        public eZet.EveLib.EveXmlModule.Corporation GetCorporations(long keyid, string vcode)
        {
            _logger.Debug("{method} {keyid}", "GetCorporations", keyid);

            try
            {
                var key = new CorporationKey(keyid, vcode);
                return key.Corporation;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetCorporations", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }


        public async Task<CharacterSheet> GetCharacterSheet(long keyid, string vcode, long characterId)
        {
            _logger.Debug("{method} {keyid} {characterId}", "GetCharacterSheet", keyid, characterId);

            try
            {
                var character = new Character((int)keyid, vcode, characterId);
                var r = await character.GetCharacterSheetAsync();
                return r.Result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetCharacterSheetBackground", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }

        public async Task<CharacterInfo> GetCharacterInfo(long keyid, string vcode, long characterId)
        {
            _logger.Debug("{method} {keyid} {characterId}", "GetCharacterInfo", keyid, characterId);

            try
            {
                var character = new Character((int)keyid, vcode, characterId);
                var z = await character.GetCharacterInfoAsync();
                return z.Result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetCharacterInfoBackground", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }

        public async Task<SkillTraining> GetSkillTraining(long keyid, string vcode, long characterId)
        {
            _logger.Debug("{method} {keyid} {characterId}", "GetSkillTraining", keyid, characterId);

            try
            {
                var character = new Character((int)keyid, vcode, characterId);
                var z = await character.GetSkillTrainingAsync();
                return z.Result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetSkillTrainingBackground", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }

        public async Task<SkillQueue> GetSkillQueue(long keyid, string vcode, long characterId)
        {
            _logger.Debug("{method} {keyid} {characterId}", "GetSkillQueue", keyid, characterId);

            try
            {
                var character = new Character((int)keyid, vcode, characterId);
                var z = await character.GetSkillQueueAsync();
                return z.Result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetSkillQueueBackground", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }

        public async Task<IndustryJobs> GetCharacterIndustryJobs(long keyid, string vcode, long characterId)
        {
            _logger.Debug("{method} {keyid} {characterId}", "GetCharacterIndustryJobs", keyid, characterId);

            try
            {
                var character = new Character((int)keyid, vcode, characterId);
                var z = await character.GetIndustryJobsAsync();
                return z.Result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetCharacterIndustryJobsBackground", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }

        public async Task<IndustryJobs> GetCorporationIndustryJobs(long keyid, string vcode, long corporationId)
        {
            _logger.Debug("{method} {keyid} {corporationid}", "GetCorporationIndustryJobsBackground", keyid, corporationId);

            try
            {
                var character = new eZet.EveLib.EveXmlModule.Corporation((int)keyid, vcode, corporationId);
                var z = await character.GetIndustryJobsAsync();
                return z.Result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetCorporationIndustryJobsBackground", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }

        public async Task<WalletJournal> GetCharacterJournal(long keyid, string vcode,
            long characterId, long fromid)
        {
            _logger.Debug("{method} {keyid} {characterid} {fromid}", "GetCharacterJournal", keyid, characterId, fromid);

            try
            {
                var entity = new eZet.EveLib.EveXmlModule.Character((int)keyid, vcode, characterId);
                var z = await entity.GetWalletJournalAsync(2560, fromid);
                return z.Result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetCharacterJournalBackground", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }

        }

        public async Task<WalletJournal> GetCorporationJournal(long keyid, string vcode,
            long corporationId, long fromid)
        {
            _logger.Debug("{method} {keyid} {corporationid} {fromid}", "GetCorporationJournal", keyid, corporationId, fromid);

            try
            {
                var entity = new eZet.EveLib.EveXmlModule.Corporation((int)keyid, vcode, corporationId);
                var z = await entity.GetWalletJournalAsync(1000, 2560, fromid);
                return z.Result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetCorporationJournalBackground", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }

        public async Task<WalletTransactions> GetCharacterWallet(long keyid, string vcode,
            long characterId, long fromid)
        {
            _logger.Debug("{method} {keyid} {characterid} {fromid}", "GetCharacterWallet", keyid, characterId, fromid);

            try
            {
                var entity = new eZet.EveLib.EveXmlModule.Character((int)keyid, vcode, characterId);
                var z = await entity.GetWalletTransactionsAsync(2560, fromid);
                return z.Result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetCharacterWalletBackground", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }

        }

        public async Task<WalletTransactions> GetCorporationWallet(long keyid, string vcode,
            long corporationId, long fromid)
        {
            _logger.Debug("{method} {keyid} {corporationid} {fromid}", "GetCorporationWallet", keyid, corporationId, fromid);

            try
            {
                var entity = new eZet.EveLib.EveXmlModule.Corporation((int)keyid, vcode, corporationId);
                var z = await entity.GetWalletTransactionsAsync(1000, 2560, fromid);
                return z.Result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "GetCorporationJournalBackground", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }

        public bool IsCorporationKey(long keyid, string vcode)
        {
            _logger.Debug("{method} {keyid}", "EveApi:IsCorporationKey", keyid);

            try
            {
                var key = new ApiKey(keyid, vcode);
                return key.KeyType == ApiKeyType.Corporation;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "EveApi:IsCorporationKey", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }

        public string GetCorporationName(long keyid, string vcode)
        {
            _logger.Debug("{method} {keyid}", "EveApi::GetCorporationName", keyid);

            try
            {
                var key = new CorporationKey(keyid, vcode);
                return key.Corporation.CorporationName;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "EveApi:GetCorporationName", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }

        public bool CheckKey(long keyid, string vcode)
        {
            _logger.Debug("{method} {keyid}", "EveApi:CheckKey", keyid);

            try
            {
                var key = new ApiKey(keyid, vcode);
                return key.KeyType == ApiKeyType.Corporation || key.KeyType == ApiKeyType.Account || key.KeyType == ApiKeyType.Character;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Authentication failure"))
                    throw new UserException(strings.ErrorAuthenticationFailure, e);

                _logger.Error("{method} {@exception}", "EveApi:CheckKey", e);

                throw new UserException(strings.ErrorCallingEveApi, e);
            }
        }


        public async Task<List<WalletJournal.JournalEntry>> GetJournalEntries(long keyid, string vcode, string characterName, long characterId, DateTime cutoffDate)
        {
            _logger.Debug("{method} {keyid}", "EveApi::GetJournalEntries", keyid);

            var ck = new eZet.EveLib.EveXmlModule.CharacterKey(keyid, vcode);
            var character = ck.Characters.FirstOrDefault(x => x.CharacterName == characterName);
            if (character == null)
                throw new UserException(string.Format(strings.ErrorAuthenticationFailureWithKey, keyid));

            var result = new List<WalletJournal.JournalEntry>();
            long fromid = 0;
            for (;;)
            {
                var partial = await character.GetWalletJournalAsync(1000, fromid);
                var toadd = partial.Result.Journal.Where(x => x.Date > cutoffDate);
                if (!toadd.Any())
                    break;

                _logger.Debug("{method} Got {count} entries", "EveApi::GetJournalEntries", toadd.Count());

                fromid = toadd.Min(x => x.RefId);
                result.AddRange(toadd);
            }

            return result;
        }

        public async Task<List<WalletJournal.JournalEntry>> GetJournalEntriesCorpo(long keyid, string vcode, long corporationId, DateTime cutoffDate)
        {
            _logger.Debug("{method} {keyid}", "EveApi::GetJournalEntriesCorpo", keyid);
            var ck = new eZet.EveLib.EveXmlModule.CorporationKey(keyid, vcode);
            if (ck.Corporation == null)
                throw new UserException(string.Format(strings.ErrorAuthenticationFailureWithKey, keyid));

            var result = new List<WalletJournal.JournalEntry>();
            long fromid = 0;
            for (;;)
            {
                var partial = await ck.Corporation.GetWalletJournalAsync(1000, 100, fromid);
                var toadd = partial.Result.Journal.Where(x => x.Date > cutoffDate);
                if (!toadd.Any())
                    break;

                _logger.Debug("{method} Got {count} entries", "EveApi::GetJournalEntriesCorpo", toadd.Count());

                fromid = toadd.Min(x => x.RefId);
                result.AddRange(toadd);
            }

            return result;
        }

        public async Task<List<WalletTransactions.Transaction>> GetTransactions(long keyid, string vcode, string characterName, long characterId, DateTime cutoffDate)
        {
            _logger.Debug("{method} {keyid}", "EveApi::GetTransactions", keyid);

            var ck = new eZet.EveLib.EveXmlModule.CharacterKey(keyid, vcode);
            var character = ck.Characters.FirstOrDefault(x => x.CharacterName == characterName);
            if (character == null)
                throw new UserException(string.Format(strings.ErrorAuthenticationFailureWithKey, keyid));

            var result = new List<WalletTransactions.Transaction>();
            long fromid = 0;
            for (;;)
            {
                var partial = await character.GetWalletTransactionsAsync(1000, fromid);
                var toadd = partial.Result.Transactions.Where(x => x.TransactionDate > cutoffDate);
                if (toadd.Count() == 0)
                    break;

                _logger.Debug("{method} Got {count} transaction entries", "EveApi::GetTransactions", toadd.Count());

                fromid = toadd.Min(x => x.TransactionId);
                result.AddRange(toadd);
            }

            return result;
        }

        public async Task<List<WalletTransactions.Transaction>> GetTransactionsCorpo(long keyid, string vcode, long corporationId, DateTime cutoffDate)
        {
            _logger.Debug("{method} {keyid}", "EveApi::GetTransactionsCorpo", keyid);

            var ck = new eZet.EveLib.EveXmlModule.CorporationKey(keyid, vcode);
            if (ck.Corporation == null)
                throw new UserException(string.Format(strings.ErrorAuthenticationFailureWithKey, keyid));

            var result = new List<WalletTransactions.Transaction>();
            long fromid = 0;
            for (;;)
            {
                var partial = await ck.Corporation.GetWalletTransactionsAsync(1000, 100, fromid);
                var toadd = partial.Result.Transactions.Where(x => x.TransactionDate > cutoffDate);
                if (toadd.Count() == 0)
                    break;

                _logger.Debug("{method} Got {count} transaction entries", "EveApi::GetTransactionsCorpo", toadd.Count());

                fromid = toadd.Min(x => x.TransactionId);
                result.AddRange(toadd);
            }

            return result;
        }
    }
}
