using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.domain
{
    public class Pilot
    {
        public long PilotId { get; set; }
        public long EveId { get; set; }

        public string Name { get; set; }

        public string Url => $"http://image.eveonline.com/Character/{EveId}_64.jpg";

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

        public virtual ICollection<Skill> Skills { get; set; }

        public List<string> TrainedSkills { get { return Skills.Select(x => x.SkillName).ToList(); } }

        public bool TrainingWarning => (TrainingQueueEnd - DateTime.UtcNow).Days == 0;

        public bool TrainingNotActive => !TrainingActive;

        public string TrainingLengthDescription =>
            $"{TimeSpanFormatter(CurrentTrainingEnd - DateTime.UtcNow)} / {TimeSpanFormatter(TrainingQueueEnd - DateTime.UtcNow)}"
            ;

        public static string TimeSpanFormatter(TimeSpan val)
        {
            if (val.Days > 0)
                return $"{val.Days}d {val.Hours}h {val.Minutes}m";

            if (val.Hours > 0)
                return $"{val.Hours}h {val.Minutes}m";

            return $"{val.Minutes}m";
        }

    }
}
