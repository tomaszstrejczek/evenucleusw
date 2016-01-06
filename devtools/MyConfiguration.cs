using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.shared;

namespace devtools
{
    public class MyConfiguration : IMyConfiguration
    {
        public bool UseSql => true;
        public string ConnectionString => "Server=tcp:evenucleusw.database.windows.net,1433;Database=evenucleusw;User ID=tomek@evenucleusw;Password=Traktor12;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
