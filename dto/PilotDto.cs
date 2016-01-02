using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.dto
{
    public class PilotDto
    {
        public long PilotId { get; set; }
        public long EveId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string CurrentTrainingNameAndLevel { get; set; }
        public DateTime CurrentTrainingEnd { get; set; }
        public DateTime TrainingQueueEnd { get; set; }
        public bool TrainingActive { get; set; }

        public int MaxManufacturingJobs { get; set; }
        public int MaxResearchJobs { get; set; }

        public int FreeManufacturingJobsNofificationCount { get; set; }
        public int FreeResearchJobsNofificationCount { get; set; }

        public long KeyInfoId { get; set; }
        public long UserId { get; set; }

        public bool TrainingWarning { get; set; }

        public bool TrainingNotActive { get; set; }

        public List<SkillDto> Skills { get; set; }
        public List<SkillInQueueDto> SkillsInQueue { get; set; }
    }
}
