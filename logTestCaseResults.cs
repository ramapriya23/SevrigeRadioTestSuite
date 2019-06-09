using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace SR_APITest
{
    public class logTestCaseResults
    {

        string dirPath;
        public void createDirectory()
        {
            DateTime _date = DateTime.Now;
            string dir = "SRAPITestSuiteResults_" + _date.ToString("dd") + _date.Month.ToString("d2") + _date.Year.ToString("d3");
            dirPath = @"C:\\" + dir;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dirPath);
            }
           
        }

        public void CreateTestCaseLogFile()
        {
            createDirectory();
            try
            {
                StreamWriter w = new StreamWriter(dirPath + "\\TestCaseResults.txt");
                
                w.Write("---------------------------------------------------------------------");
                w.Write("\r\nTEST CASE RESULTS");
                w.WriteLine(" {0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
                w.Write("---------------------------------------------------------------------");

                w.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logMessage)
        {
            try
            {
                StreamWriter w = new StreamWriter(dirPath + "\\TestCaseResults.txt",true);

                w.WriteLine(logMessage);

                w.Close();
            }
            catch (Exception e)
            {
            }
        }
    }
}
