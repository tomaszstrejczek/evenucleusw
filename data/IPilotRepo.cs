using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ts.domain;

namespace ts.data
{
    public interface IPilotRepo
    {
        Task Update(long userid, UserDataDto data);
        Task<ICollection<Pilot>> GetAll(long userid);
        Task<ICollection<Pilot>> GetByKeyInfoId(long keyinfoid);
        Task SetFreeManufacturingJobsNofificationCount(long pilotid, int value);
        Task SetFreeResearchJobsNofificationCount(long pilotid, int value);
        Task CheckKey(long keyid, string vcode);
        Task SimpleUpdateFromKey(long userid, long keyinfoid, long keyid, string vcode);
        Task DeleteByKey(long keyinfoid);
    }
}
