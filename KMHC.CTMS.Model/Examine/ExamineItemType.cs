using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Examine
{
    public class ExamineItemType
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public List<ExamineTemplateItems> Items { get; set; }

        public ExamineItemType()
        {
            Items = new List<ExamineTemplateItems>();
        }
    }
}
