using System;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SR_APITest
{

    [TestClass]
    public class SRTestSuite
    {
        [TestMethod]

        public void BeginTestSuite()
        {

            //-------TestCase 1 : Test Sveriges Radio API URL response status ----------------

            string xml = TestAPIStatus("http://api.sr.se/api/v2/channels");

            //---------------------------------------------------------------------------------

            //if Successful, test further

            if (xml != null)
            {
                SRChannelTestSuite(xml);
            }


        }
        public void SRChannelTestSuite(string xmlAPI)
        {
            SverigesRadio sr1 = new SverigesRadio();
            sr1.xml = xmlAPI;
            do
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(sr1.xml);
                //XmlNodeList pageInfo = doc.GetElementsByTagName("pagination");
                XmlNodeList xnlist = doc.SelectNodes("//pagination");
                foreach (XmlNode pageInfo in xnlist)
                {

                    sr1.currentPage = Convert.ToInt32(pageInfo["page"].InnerText.Trim());
                    sr1.pageSize = Convert.ToInt32(pageInfo["size"].InnerText.Trim());
                    sr1.totalPages = Convert.ToInt32(pageInfo["totalpages"].InnerText.Trim());
                    if (sr1.currentPage != sr1.totalPages)
                    {
                        sr1.nextPage = pageInfo["nextpage"].InnerText.Trim();
                    }
                    if (sr1.currentPage != 1)
                    {
                        sr1.previousPage = pageInfo["previouspage"].InnerText.Trim();
                    }
                }
                XmlNodeList channelList = doc.SelectNodes("//channels//channel");

                int count = 0;
                string chanID = "";
                sr1.channels = new SRChannel[channelList.Count];
                foreach (XmlNode channelInfo in channelList)
                {
                    try
                    {


                        SRChannel channel = new SRChannel();

                        channel.channelId = chanID = channelInfo.Attributes["id"].Value;
                        channel.channelName = channelInfo.Attributes["name"].Value;
                        channel.image = channelInfo["image"].InnerText;

                        //----------Check if image load is successful---------

                        TestAPIStatus(channel.image);

                        //----------------------------------------------------
                        channel.imageTemplate = channelInfo["imagetemplate"].InnerText.Trim();

                        channel.color = channelInfo["color"].InnerText.Trim();
                        channel.tagline = channelInfo["tagline"].InnerText.Trim();
                        channel.siteURL = channelInfo["siteurl"].InnerText.Trim();

                        //----------Check loading of siteURL is successful--------

                        TestAPIStatus(channel.siteURL);

                        //----------------------------------------------------

                        XmlNodeList laList = doc.SelectNodes("//channel[@id='" + channel.channelId + "']//liveaudio");
                        foreach (XmlNode xn in laList)
                        {
                            channel.liveAudioURL = xn["url"].InnerText.Trim();
                            channel.statKey = xn["statkey"].InnerText.Trim();
                        }

                        //-----------check live Audio stream-------------------

                        TestAPIStatus(channel.siteURL);

                        //--------------------------------------------------

                        // channel.statKey = liveAudio[k].ChildNodes.Item(1).InnerText.Trim();

                        //}


                        channel.scheduleURL = channelInfo["scheduleurl"].InnerText.Trim();
                        //---------------check schedule API for this channel------
                        try
                        {
                            sr1.channels[sr1.currentPage].schedule = scheduleTest(TestAPIStatus(channel.scheduleURL), channel); //---if NullReferenceExcetion,  means scheduleTest has failed and returned null 
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Schedules for channel ID : " + channel.channelId + "are missing elements");
                        }
                        //-----------------------------------------------------
                        channel.channelType = channelInfo["channeltype"].InnerText.Trim();
                        channel.xmlTvId = channelInfo["xmltvid"].InnerText.Trim();
                        sr1.channels[count++] = channel;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Elements missing for channel ID :" + chanID);
                    }
                }
                //----------Check statusCode of the next page---------
                if (sr1.currentPage != sr1.totalPages)
                    sr1.xml = TestAPIStatus(sr1.nextPage);
                if (sr1.xml == null)
                {
                    Console.WriteLine("URL to nextpage failed");
                    return;
                }
                //----------------------------------------------------

            } while (sr1.currentPage < sr1.totalPages);



        }

        public SRSchedule scheduleTest(string xml, SRChannel channel)
        {
            SRSchedule srs = new SRSchedule();
            srs.xml = xml;
            do
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(srs.xml);
                XmlNodeList pageList = doc.SelectNodes("//pagination");
                foreach (XmlNode pageInfo in pageList)
                {

                    srs.currentPage = Convert.ToInt32(pageInfo["page"].InnerText.Trim());
                    srs.pageSize = Convert.ToInt32(pageInfo["size"].InnerText.Trim());
                    srs.totalPages = Convert.ToInt32(pageInfo["totalpages"].InnerText.Trim());
                    if (srs.currentPage != srs.totalPages)
                    {
                        srs.nextPage = pageInfo["nextpage"].InnerText.Trim();
                    }
                    if (srs.currentPage != 1)
                    {
                        srs.previousPage = pageInfo["previouspage"].InnerText.Trim();
                    }
                }
                XmlNodeList scheduleList = doc.SelectNodes("//schedule//scheduledepisode");

                srs.episodes = new SREpisode[scheduleList.Count];

                string progID = "";
                int count = 0;
                foreach (XmlNode scheduleInfo in scheduleList)
                {
                    try
                    {
                        SREpisode episode = new SREpisode();
                        episode.programID = progID = scheduleInfo["program"].Attributes["id"].Value;
                        episode.programName = scheduleInfo["program"].Attributes["name"].Value;
                        episode.episodeID = scheduleInfo["episodeid"].InnerText.Trim();
                        episode.title = scheduleInfo["title"].InnerText.Trim();
                        episode.subtitle = scheduleInfo["subtitle"].InnerText.Trim();
                        episode.description = scheduleInfo["description"].InnerText.Trim();
                        episode.startTime = scheduleInfo["starttimeutc"].InnerText.Trim();
                        episode.endTime = scheduleInfo["endtimeutc"].InnerText.Trim();

                        episode.channelID = scheduleInfo["channel "].Attributes["id"].Value;
                        episode.channelName = scheduleInfo["channel "].Attributes["name"].Value;


                        //--------------check if program is displayed under the correct channel
                        try
                        {
                            Assert.AreEqual(episode.channelID, channel.channelId);
                            Assert.AreEqual(episode.channelName, channel.channelName);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Program Schedule does not belong to the correct channel\n" + e.Message);

                        }

                        //----------------------------------------------------------------------
                        episode.imageURL = scheduleInfo["imageurl"].InnerText.Trim();

                        //----------Check if image load is successful---------

                        TestAPIStatus(episode.imageURL);

                        //----------------------------------------------------
                        episode.imageURLTemplate = scheduleInfo["imageurltemplate"].InnerText.Trim();


                        srs.episodes[count++] = episode;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + "\nProgram ID :  " + progID + "  missing elements");
                    }



                }




                //----------Check statusCode of the next page---------

                srs.xml = TestAPIStatus(srs.nextPage);
                if (srs.xml == null)
                {
                    Console.WriteLine("URL to nextpage failed");
                    return null;
                }
                //----------------------------------------------------
                //if (srs.currentPage == srs.totalPages) break;
            } while (srs.currentPage < srs.totalPages);


            return srs;
        }
        public string TestAPIStatus(string api)
        {

            string API = api;

            var client = new RestClient(API);

            var request = new RestRequest(Method.GET);

            IRestResponse restResponse = client.Execute(request);

            string xml = restResponse.Content.ToString();

            string status = restResponse.StatusCode.ToString();
            try
            {
                Assert.AreEqual(status, "OK");
            }
            catch (Exception statusFail)
            {
                Console.WriteLine("URL : " + API + "\nStatus Code : " + status + "\n" + statusFail.Message);

            }

            return xml;

        }

        //public object getPaginationInfo(XmlNodeList pageInfo, Object sr1)
        //{
        //    for (int i = 0; i < pageInfo.Count; i++)
        //    {

        //        sr1.currentPage = Convert.ToInt32(pageInfo[i].ChildNodes.Item(0).InnerText.Trim());
        //        sr1.pageSize = Convert.ToInt32(pageInfo[i].ChildNodes.Item(1).InnerText.Trim());
        //        sr1.totalPages = Convert.ToInt32(pageInfo[i].ChildNodes.Item(3).InnerText.Trim());
        //        sr1.nextPage = pageInfo[i].ChildNodes.Item(4).InnerText.Trim();
        //        if (sr1.currentPage != 1)
        //        {
        //            sr1.previousPage = pageInfo[i].ChildNodes.Item(5).InnerText.Trim();
        //        }
        //    }
        //}

    }
}
