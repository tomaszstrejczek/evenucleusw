using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.data;
using ts.domain;

namespace ts.services
{
    public class BackgroundUpdate: IBackgroundUpdate
    {
        private readonly IEvePilotDataService _evePilotDataService;
        private readonly IPilotRepo _pilotRepo;
        private readonly ICorporationRepo _corporationRepo;
        private readonly ISkillRepo _skillRepo;
        private readonly ISkillInQueueRepo _skillInQueueRepo;

        public BackgroundUpdate(IEvePilotDataService evePilotDataService, IPilotRepo pilotRepo, ICorporationRepo corporationRepo, ISkillRepo skillRepo, ISkillInQueueRepo skillInQueueRepo)
        {
            _evePilotDataService = evePilotDataService;
            _pilotRepo = pilotRepo;
            _corporationRepo = corporationRepo;
            _skillRepo = skillRepo;
            _skillInQueueRepo = skillInQueueRepo;
        }

        public async Task Update(long userid)
        {
            var userData = new UserDataDto();
            var eveData = await _evePilotDataService.Get(userid);
            userData.Pilots = eveData.Item1;
            userData.Corporations = eveData.Item2;
            userData.CachedUntilUTC = eveData.Item3;
            userData.Jobs = new List<Job>();

            await _pilotRepo.Update(userid, userData);
            await _corporationRepo.Update(userid, userData);
            await _skillRepo.Update(userid, userData);
            await _skillInQueueRepo.Update(userid, userData);
        }
    }
}
