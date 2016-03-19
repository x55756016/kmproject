using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.DAL.Database
{
    public partial class HPN_TEMPLATE
    {
        public List<HPN_TEMPLATEITEM> Questions { get; set; }

        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime? TestTime { get; set; }

        /// <summary>
        /// 测试结果
        /// </summary>
        public string TestResult { get; set; }

        /// <summary>
        /// 测试分数
        /// </summary>
        public double TestScore { get; set; }
    }
}
