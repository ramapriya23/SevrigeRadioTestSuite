using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Schema;



namespace SR_APITest
{

    
    public class SverigesRadio
    {
        public string xml { get; set; }
        public int currentPage { get; set; }
        public string previousPage { get; set; }
        public string nextPage { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }

        public SRChannel[] channels { get; set; }


    }

    
}