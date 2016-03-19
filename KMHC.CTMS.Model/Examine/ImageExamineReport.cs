using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Examine
{
    public class ImageExamineReport
    {
        public string Id { get; set; }

        public string TemplateType { get; set; }

        public Nullable<DateTime> CheckDate { get; set; }

        public string Dept { get; set; }

        public string Part { get; set; }

        public string Diagnose { get; set; }

        public string See { get; set; }

        public Nullable<DateTime> CreateDateTime { get; set; }

        public string CreateUserId { get; set; }

        public string CreateUserName { get; set; }

        public Nullable<DateTime> EditDateTime { get; set; }

        public string EditUserId { get; set; }

        public string EditUserName { get; set; }

        public int IsDeleted { get; set; }

        public string ReportDoctor { get; set; }

        public string CheckDoctor { get; set; }

        public string ReportDetail { get; set; }

        public string Remark { get; set; }

        public string UserId { get; set; }
    }
}
