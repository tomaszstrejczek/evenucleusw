using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZet.EveLib.EveXmlModule.Models;
using eZet.EveLib.EveXmlModule.Models.Character;
using Serilog;

using ts.data;
using ts.domain;

namespace ts.services
{
    public class JobsService : IJobService
    {
        private IKeyInfoRepo _userRepo;
        private IEveApi _eveApi;
        private ILogger _logger;
        private ITypeNameDict _typeNameDict;
        private ICharacterNameDict _characterNameDict;

        public JobsService(IKeyInfoRepo userRepo, IEveApi eveApi, ILogger logger, ITypeNameDict typeNameDict, ICharacterNameDict characterNameDict)
        {
            _userRepo = userRepo;
            _eveApi = eveApi;
            _logger = logger;
            _typeNameDict = typeNameDict;
            _characterNameDict = characterNameDict;
        }
        public async Task<Tuple<List<Job>, DateTime>> Get(long userid)
        {
            var list = new List<Job>();
            var cachedUntilUTC = DateTime.UtcNow;

            _logger.Debug("{method}", "JobsService::Get");
            try
            {
                var keys = await _userRepo.GetKeys(userid);

                var visited = new HashSet<string>();
                var jobsTaskList = new List<Task<EveXmlResponse<IndustryJobs>>>();
                foreach (var k in keys)
                {
                    bool corpoKey = _eveApi.IsCorporationKey(k.KeyId, k.VCode);

                    if (corpoKey)
                    {
                        var corporation = _eveApi.GetCorporations(k.KeyId, k.VCode);
                        if (visited.Contains(corporation.CorporationName))
                            continue;
                        visited.Add(corporation.CorporationName);

                        jobsTaskList.Add(corporation.GetIndustryJobsAsync());
                    }
                    else
                    {
                        var characters = _eveApi.GetCharacters(k.KeyId, k.VCode);
                        foreach (var character in characters)
                        {
                            if (visited.Contains(character.CharacterName))
                                continue;
                            visited.Add(character.CharacterName);

                            var task = character.GetIndustryJobsAsync();
                            jobsTaskList.Add(task);
                            var z = (await task).Result;
                        }
                    }
                }

                foreach (var jobTask in jobsTaskList)
                {
                    var jobs = await jobTask;
                    cachedUntilUTC = cachedUntilUTC < jobs.CachedUntil ? cachedUntilUTC : jobs.CachedUntil;

                    var typeNameDict =
                        (await _typeNameDict.GetById(jobs.Result.Jobs.Select(x => (long)x.BlueprintTypeId)))
                            .ToDictionary(key => key.Item1, value => value.Item2);

                    foreach (var j in jobs.Result.Jobs)
                    {
                        var typeName = typeNameDict[(long)j.BlueprintTypeId];
                        var runningTime = DateTime.UtcNow > j.StartDate
                            ? DateTime.UtcNow - j.StartDate
                            : new TimeSpan();
                        var totalTime = j.EndDate > j.StartDate ? j.EndDate - j.StartDate : new TimeSpan();
                        int percentage = 0;
                        if (totalTime.TotalSeconds > 0)
                            percentage = Math.Min((int)(100.0 * runningTime.TotalSeconds / totalTime.TotalSeconds), 100);

                        var job = new Job()
                        {
                            JobCompleted = j.CompletedDate != new DateTime(),
                            JobDescription = string.Format("{0} {1} {2}", typeName, GetActivityAnnotation(j.ActivityId), j.Runs),
                            JobEnd = j.EndDate > DateTime.UtcNow ? j.EndDate : DateTime.UtcNow,
                            Owner = j.InstallerName,
                            PercentageOfCompletion = percentage,
                            Url = string.Format("http://image.eveonline.com/Type/{0}_64.png", j.BlueprintTypeId),
                            IsManufacturing = j.ActivityId == 1
                        };
                        list.Add(job);
                    }
                }
            }
            catch (UserException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.Error("{method} {@exception}", "JobService::Get", e);
                throw new UserException(strings.ErrorCallingEveApi, e);
            }

            return new Tuple<List<Job>, DateTime>(list, cachedUntilUTC);
        }

        public static string GetActivityAnnotation(int activity)
        {
            switch (activity)
            {
                case 1:
                    return "x";
                case 3:
                    return "TE";
                case 4:
                    return "ME";
                case 5:
                    return "CP";
                case 7:
                    return "RE";
                default:
                    return string.Format("?{0}?", activity);
            }
        }

    }
}
