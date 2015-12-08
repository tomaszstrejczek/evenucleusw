using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.shared;

namespace ts.data
{
    public class MyConfiguration: IMyConfiguration
    {
        public bool UseSql => true;
        public string ConnectionString => "";
    }
}
