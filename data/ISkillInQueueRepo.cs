using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.domain;
using ts.dto;

namespace ts.data
{
    public interface ISkillInQueueRepo
    {
        Task Update(long userId, UserDataDto data);
        Task<List<SkillInQueueDto>> GetForPilot(long pilotId);
    }
}
