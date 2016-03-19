using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Examine
{
    public class ExamineTemplates
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 模块类型（0、影像检验模版维护）
        /// </summary>
        public int Type { get; set; }


        /// <summary>
        /// 模块类型（0、影像检验模版维护）
        /// </summary>
        public string DicType { get; set; }



        /// <summary>
        /// 模版编码
        /// </summary>
        public string TemplateCode { get; set; }

        public string CreateUserId { get; set; }

        public string CreateUserName { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string EditUserId { get; set; }

        public string EditUserName { get; set; }

        public DateTime EditDateTime { get; set; }

        public int IsDeleted { get; set; }

        public List<ExamineTemplateItems> ItemTypes { get; set; }

        public ExamineTemplates()
        {
            ItemTypes = new List<ExamineTemplateItems>();
        }
    }
}
