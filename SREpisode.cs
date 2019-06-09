using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_APITest
{
    public class SREpisode
    {
        public string episodeID { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string description { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string programID { get; set; }
        public string programName { get; set; }
        public string channelID { get; set; }
        public string channelName { get; set; }
        public string imageURL { get; set; }
        public string imageURLTemplate { get; set; }
    }

}
