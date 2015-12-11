using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.dto
{
    public class CorporationDto
    {
        public long CorporationId { get; set; }

        public long EveId { get; set; }
        public string Name { get; set; }

        public long KeyInfoId { get; set; }
        public long UserId { get; set; }

        public String Url { get; set; }
    }
}
