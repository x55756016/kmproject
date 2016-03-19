using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class HPN_Template
    {
        public decimal ID { get; set; }
        public string Title { get; set; }
        public string TemplateQuarry { get; set; }
        public string Label { get; set; }
        public string ActionName { get; set; }
        public string Icon { get; set; }
        public decimal Index { get; set; }
        public decimal Enabled { get; set; }
        public decimal Type { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public System.DateTime LastModifyTime { get; set; }
        public string LastModifyUser { get; set; }
        public List<HPN_TemplateItem> Items { get; set; }
    }
}
