using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.dto
{
    public class KeyInfoDto
    {
        public long KeyInfoId { get; set; }
        public long KeyId { get; set; }
        public string VCode { get; set; }
        public long UserId { get; set; }
        public List<PilotDto> Pilots { get; set; }
        public List<CorporationDto> Corporations { get; set; }
    }
}
