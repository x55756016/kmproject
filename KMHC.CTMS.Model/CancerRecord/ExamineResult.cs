using System.Collections;
using System.Collections.Generic;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class ExamineResult
    {
        public int ResultId { get; set; }

        public int ExamId { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public int ItemId { get; set; }

        public string ItemCode { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 检验结果
        /// </summary>
        public IEnumerable<LabTestResult> LabTestResults { get; set; }
    }
}
