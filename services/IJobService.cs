using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ts.domain;

namespace ts.services
{
    public interface IJobService
    {
        /// <returns>List of JobInfo & cachedUntilUTC</returns>
        Task<Tuple<List<Job>, DateTime>> Get(long userid);
    }
}
