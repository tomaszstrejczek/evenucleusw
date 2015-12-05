using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.db
{
    public enum JobStatus
    {
        Started,
        Error,
        Completed
    }
    public class Job
    {
        public int JobId { get; set; }
        public long UserId { get; set; }
        public string Owner { get; set; }
        public string JobDescription { get; set; }
        public string Url { get; set; }
        public DateTime JobEnd { get; set; }
        public int PercentageOfCompletion { get; set; } // 0-100
        public bool JobCompleted { get; set; }
        public bool IsManufacturing { get; set; }

        public string DurationDescription
        {
            get
            {
                return $"{Pilot.TimeSpanFormatter(JobEnd - DateTime.UtcNow)} ({PercentageOfCompletion}%)";
            }
        }
    }
}
