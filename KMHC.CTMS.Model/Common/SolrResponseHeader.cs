using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Common
{
    public class SolrResponseHeader
    {
        public SolrResponseHeader()
        {
            @params = new SolrSelectParams();
        }

        public int status { get; set; }

        public int QTime { get; set; }

        public SolrSelectParams @params { get; set; }
    }
}
