using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Serilog;

using ts.data;
using ts.domain;

namespace ts.services
{
    public class PilotService : IPilotService
    {
        private IKeyInfoRepo _keyRepo;
        private IEveApi _eveApi;
        private ILogger _logger;
        private ITypeNameDict _typeNameDict;

        public PilotService(IKeyInfoRepo keyRepo, IEveApi eveApi, ILogger logger, ITypeNameDict typeNameDict)
        {
            _keyRepo = keyRepo;
            _eveApi = eveApi;
            _logger = logger;
            _typeNameDict = typeNameDict;
        }

        public async Task<Tuple<List<Pilot>, List<Corporation>, DateTime>> Get(long userid)
        {
            var list = new List<Pilot>();
            var listCorpo = new List<Corporation>();
            var cachedUntilUTC = DateTime.UtcNow.AddHours(2);

            _logger.Debug("{method}", "PilotService::Get");

            try
            {
                var keys = await _keyRepo.GetKeys(userid);
                foreach (var k in keys)
                {
                    if (_eveApi.IsCorporationKey(k.KeyId, k.VCode))
                    {
                        var ci = new Corporation();
                        ci.KeyInfoId = k.KeyInfoId;
                        var corpo = _eveApi.GetCorporations(k.KeyId, k.VCode);
                        ci.Name = corpo.CorporationName;
                        ci.CorporationId = corpo.CorporationId;
                        listCorpo.Add(ci);
                        continue;
                    }

                    var characters = _eveApi.GetCharacters(k.KeyId, k.VCode);
                    foreach (var character in characters)
                    {
                        if (list.Count(x => x.Name == character.CharacterName) > 0)
                            continue;

                        var sheet = await character.GetCharacterSheetAsync();
                        var info = await character.GetCharacterInfoAsync();
                        var skillInTraining = await character.GetSkillTrainingAsync();
                        var skillQueue = await character.GetSkillQueueAsync();

                        var dt =
                            new DateTime[]
                            {sheet.CachedUntil, info.CachedUntil, skillInTraining.CachedUntil, skillQueue.CachedUntil}
                                .Min();
                        cachedUntilUTC = cachedUntilUTC < dt ? cachedUntilUTC : dt;

                        const long massProductionTypeId = 3387;
                        const long advancedMassProductionTypeId = 24625;
                        const long laboratoryOperationTypeId = 3406;
                        const long advancedLaboratoryOperationTypeId = 24624;

                        var maxManufacturingJobs = 1 +
                                                   sheet.Result.Skills.Where(
                                                       x =>
                                                           x.TypeId == massProductionTypeId ||
                                                           x.TypeId == advancedMassProductionTypeId).Sum(x => x.Level);
                        var maxResearchJobs = 1 +
                                                   sheet.Result.Skills.Where(
                                                       x =>
                                                           x.TypeId == laboratoryOperationTypeId ||
                                                           x.TypeId == advancedLaboratoryOperationTypeId).Sum(x => x.Level);


                        string typename = "";
                        if (skillInTraining.Result.TypeId > 0)
                        {
                            var tp = await _typeNameDict.GetById(new long[] { skillInTraining.Result.TypeId });
                            typename = tp[0].Item2;
                        }
                        var p = new Pilot()
                        {
                            Name = character.CharacterName,
                            CurrentTrainingEnd = skillInTraining.Result.EndTime,
                            CurrentTrainingNameAndLevel = string.Format("{0} {1}", typename, skillInTraining.Result.ToLevel),
                            TrainingQueueEnd = skillQueue.Result.Queue.Max(x => x.EndTime),
                            TrainingActive = skillInTraining.Result.IsTraining,
                            EveId = character.CharacterId,
                            MaxManufacturingJobs = maxManufacturingJobs,
                            MaxResearchJobs = maxResearchJobs,
                            KeyInfoId = k.KeyInfoId
                        };
                        var typenames =
                            (await _typeNameDict.GetById(sheet.Result.Skills.Select(x => (long)x.TypeId))).ToDictionary
                                (key => key.Item1, value => value.Item2);
                        p.Skills = sheet.Result.Skills.Select(x => new Skill()
                        {
                            PilotId = p.PilotId,
                            SkillName = $"{typenames[(long) x.TypeId]}",
                            Level = x.Level
                        }).ToList();

                        list.Add(p);
                    }
                }
            }
            catch (UserException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.Error("{method} {@exception}", "PilotService::Get", e);
                if (e.ToString().Contains("Illegal page") && e.ToString().Contains("access"))
                    throw new UserException(strings.ErrorAccessMask, e);
                throw new UserException(strings.ErrorCallingEveApi, e);
            }

            return new Tuple<List<Pilot>, List<Corporation>, DateTime>(list, listCorpo, cachedUntilUTC);
        }
    }
}
