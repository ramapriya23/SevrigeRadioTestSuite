using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Schema;



namespace UnitTestAPI
{

    public class XMLTest
    {
        public string page { get; set; }
        public string size { get; set; }
        public string totalhits { get; set; }
        public string totalpages { get; set; }
        public string nextpage { get; set; }

    }
    public class SverigesRadioAPI
    {
        public string xml { get; set; }
        public int currentPage { get; set; }
        public string previousPage { get; set; }
        public string nextPage { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }

        public SRChannel[] channels { get; set; }


    }

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

    public class SRSchedule
    {
        public string xml { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public string previousPage { get; set; }
        public string nextPage { get; set; }
        public int pageSize { get; set; }
        public SREpisode[] episodes { get; set; }
    }

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