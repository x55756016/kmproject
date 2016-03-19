using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Common
{
    public class SolrSelectDocs
    {
        public string uri { get; set; }

        public string size { get; set; }

        public string lastModified { get; set; }

        public string text { get; set; }

        public string fileName { get; set; }

        public string author { get; set; }

        public string title { get; set; }

        public string Testmeta { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
