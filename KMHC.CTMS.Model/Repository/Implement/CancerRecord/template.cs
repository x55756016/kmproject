using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class template
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Persent { get; set; }

        public string Question { get; set; }

        public string Help { get; set; }

        public int Type { get; set; }

        public List<templateitem> Content { get; set; }

        public bool IsLast { get; set; }

        public bool IsFirst { get; set; }

        public string TemplateNo { get; set; }

        public bool IsFinish { get; set; }

        public string Msg { get; set; }

        public string CurrentId { get; set; }

        public string CurrentValue { get; set; }

        public string PrevItemName { get; set; }

        public string UserId { get; set; }
    }

    public class templateitem
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public string Name { get; set; }

        public string IdAttr { get; set; }
    }

    public class subresult
    {
        public string RESULT { get; set; }

        public double Score { get; set; }

        public string Remark { get; set; }
    }
}
