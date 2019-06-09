using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_APITest
{
    public class SRChannel
    {

        public string channelId { get; set; }
        public string channelName { get; set; }
        public string image { get; set; }
        public string imageTemplate { get; set; }
        public string color { get; set; }
        public string tagline { get; set; }
        public string siteURL { get; set; }
        public string liveAudioURL { get; set; }
        public string statKey { get; set; }
        public string scheduleURL { get; set; }
        public string channelType { get; set; }
        public string xmlTvId { get; set; }

        public SRSchedule schedule { get; set; }


    }
}
