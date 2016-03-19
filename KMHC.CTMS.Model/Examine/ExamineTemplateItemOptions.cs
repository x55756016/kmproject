using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Examine
{
    public class ExamineTemplateItemOptions
    {
        public string Id { get; set; }

        public string ExamineItemId { get; set; }

        public string ExamineItemName { get; set; }

        public string Description { get; set; }

        public string CreateUserId { get; set; }

        public string CreateUserName { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string EditUserId { get; set; }

        public string EditUserName { get; set; }

        public DateTime EditDateTime { get; set; }

        public int IsDeleted { get; set; }
    }
}
