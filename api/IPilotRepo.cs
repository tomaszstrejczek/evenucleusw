using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ts.db;

namespace ts.api
{
    public interface IPilotRepo
    {
        Task Update(long userid, UserDataDto data);
        Task<ICollection<Pilot>> GetAll(long userid);
        Task SetFreeManufacturingJobsNofificationCount(long pilotid, int value);
        Task SetFreeResearchJobsNofificationCount(long pilotid, int value);
        Task SimpleUpdateFromKey(long userid, long keyid, string vcode);
        Task DeleteByKey(long keyinfoid);
    }
}
