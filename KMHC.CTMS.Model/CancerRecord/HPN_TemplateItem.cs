using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class HPN_TemplateItem
    {
        public decimal ID { get; set; }
        public string TemplateName { get; set; }
        public decimal Index { get; set; }
        public decimal TagType { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Html { get; set; }
        public string Remark { get; set; }
        public string NextItem { get; set; }
        public List<HPN_TemplateItemOptions> Options { get; set; }
    }
}
