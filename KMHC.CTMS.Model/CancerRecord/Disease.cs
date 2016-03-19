using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class Disease
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string TypeCode { get; set; }
    }

    public class DiseaseType
    {
        public int Id { get; set; }
        public string CategoryCode { get; set; }

        public string StartCode { get; set; }

        public string EndCode { get; set; }

        public string DiseaseName { get; set; }

        public int ParentId { get; set; }


    }

    public class DiseaseTypeTree
    {
        public string text { get; set; }

        public string value { get; set; }

        public string href { get; set; }

        public string tags { get; set; }

        public IEnumerable<DiseaseTypeTree> nodes { get; set; }

    }
}
