using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Examine
{
    public class ExamineTemplateItems
    {
        public string Id { get; set; }

        public string ExamineTemplateId { get; set; }

        public string TemplateName { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public string CreateUserId { get; set; }

        public string CreateUserName { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string EditUserId { get; set; }

        public string EditUserName { get; set; }

        public DateTime EditDateTime { get; set; }

        public int IsDeleted { get; set; }

        public List<ExamineTemplateItemOptions> Options { get; set; }

        public ExamineTemplateItems()
        {
            Options = new List<ExamineTemplateItemOptions>();
        }
    }
}
