using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.db;

namespace ts.api
{
    public interface ISkillRepo
    {
        Task Update(long userId, UserDataDto data);
        Task<List<string>> GetForPilot(long eveId);
    }
}
