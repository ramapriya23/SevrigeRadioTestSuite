using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_APITest
{
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
}
