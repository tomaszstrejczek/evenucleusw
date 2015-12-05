using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eZet.EveLib.EveXmlModule;
using eZet.EveLib.EveXmlModule.Models.Account;
using eZet.EveLib.EveXmlModule.Models.Character;

namespace ts.api
{
    public interface IEveApi
    {
        /// <summary>
        /// Verifies given key and verification code. If the key is correct then returns characters associated with the key
        /// Throws communication related exception.
        /// </summary>
        /// <param name="keyid">EVE Api key id</param>
        /// <param name="vcode">EVE Api verification code</param>
        List<Character> GetCharacters(long keyid, string vcode);

        eZet.EveLib.EveXmlModule.Corporation GetCorporations(long keyid, string vcode);

        bool IsCorporationKey(long keyid, string vcode);
        string GetCorporationName(long keyid, string vcode);

        bool CheckKey(long keyid, string vcode);

        Task<CharacterSheet> GetCharacterSheet(long keyid, string vcode, long characterId);
        Task<CharacterList.CharacterInfo> GetCharacterInfo(long keyid, string vcode, long characterId);
        Task<SkillTraining> GetSkillTraining(long keyid, string vcode, long characterId);
        Task<SkillQueue> GetSkillQueue(long keyid, string vcode, long characterId);

        Task<IndustryJobs> GetCharacterIndustryJobs(long keyid, string vcode, long characterId);
        Task<IndustryJobs> GetCorporationIndustryJobs(long keyid, string vcode, long corporationId);

        Task<WalletJournal> GetCharacterJournal(long keyid, string vcode, long characterId, long fromid);
        Task<WalletJournal> GetCorporationJournal(long keyid, string vcode, long corporationId, long fromid);

        Task<WalletTransactions> GetCharacterWallet(long keyid, string vcode, long characterId, long fromid);
        Task<WalletTransactions> GetCorporationWallet(long keyid, string vcode, long corporationId, long fromid);


        /// <summary>
        /// Returns journal entries for given character. Only the entries newer than cutoffDate are returned. Provide really old date to get all entires
        /// </summary>
        /// <param name="keyid">EVE Api key id</param>
        /// <param name="vcode">EVE Api verification code</param>
        /// <param name="characterName">Valid character name for given key</param>
        /// <param name="characterId">character id</param>
        /// <param name="cutoffDate">Only entries newet than this date are retuned</param>
        /// <returns></returns>
        Task<List<WalletJournal.JournalEntry>> GetJournalEntries(long keyid, string vcode, string characterName, long characterId, DateTime cutoffDate);

        /// <summary>
        /// Returns journal entries for given corpo. Only the entries newer than cutoffDate are returned. Provide really old date to get all entires
        /// </summary>
        /// <param name="keyid">EVE Api key id</param>
        /// <param name="vcode">EVE Api verification code</param>
        /// <param name="corporationId">Corporation ID</param>
        /// <param name="cutoffDate">Only entries newet than this date are retuned</param>
        /// <returns></returns>
        Task<List<WalletJournal.JournalEntry>> GetJournalEntriesCorpo(long keyid, string vcode, long corporationId, DateTime cutoffDate);

        /// <summary>
        /// Returns wallet transactions for given character. Only the entries newer than cutoffDate are returned. Provide really old date to get all entires
        /// </summary>
        /// <param name="keyid">EVE Api key id</param>
        /// <param name="vcode">EVE Api verification code</param>
        /// <param name="characterName">Valid character name for given key</param>
        /// <param name="characterId">characterId</param>
        /// <param name="cutoffDate">Only entries newet than this date are retuned</param>
        /// <returns></returns>
        //Task<List<WalletTransaction>> GetTransactions(long keyid, string vcode, string characterName, long characterId, DateTime cutoffDate);

        /// <summary>
        /// Returns wallet transactions for given character. Only the entries newer than cutoffDate are returned. Provide really old date to get all entires
        /// </summary>
        /// <param name="keyid">EVE Api key id</param>
        /// <param name="vcode">EVE Api verification code</param>
        /// <param name="corproationId">corporation id</param>
        /// <param name="cutoffDate">Only entries newet than this date are retuned</param>
        /// <returns></returns>
        //Task<List<WalletTransaction>> GetTransactionsCorpo(long keyid, string vcode, long corporationId, DateTime cutoffDate);

    }
}
