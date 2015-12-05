using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.db
{
    public class Corporation
    {
        public long CorporationId { get; set; }

        public long EveId { get; set; }
        public string Name { get; set; }

        public long KeyInfoId { get; set; }
        public long UserId { get; set; }

        public String Url
        {
            get { return String.Format("http://image.eveonline.com/Corporation/{0}_64.png", EveId); }
        }
    }
}
