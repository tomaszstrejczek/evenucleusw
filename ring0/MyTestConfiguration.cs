using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.shared;

namespace ring0
{
    public class MyTestConfiguration: IMyConfiguration
    {
        public bool UseSql => false;
        public string ConnectionString => "";
    }
}
