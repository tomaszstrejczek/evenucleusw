using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.shared
{
    public interface IMyConfiguration
    {
        bool UseSql { get; }
        string ConnectionString { get; }
    }
}
