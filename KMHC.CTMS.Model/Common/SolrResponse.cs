using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Common
{
    public class SolrResponse
    {
        public SolrResponse()
        {
            docs = new List<SolrSelectDocs>();
        }

        public int numFound { get; set; }

        public int start { get; set; }

        public List<SolrSelectDocs> docs { get; set; }
    }
}
