/*
 * 描述:影像学检查结果
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2015/11/6  邓柏生                                      创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class ImageExamination
    {
        /// <summary>
        /// 影像检查ID
        /// </summary>
        public string ImageExamID { get; set; }

        /// <summary>
        /// 就诊史ID
        /// </summary>
        public string HistoryID { get; set; }

        /// <summary>
        /// 报告单类型
        /// </summary>
        public string CheckType { get; set; }

        /// <summary>
        /// 检查日期
        /// </summary>
        public Nullable<System.DateTime> CheckTime { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        public string CheckPosition { get; set; }

        /// <summary>
        /// 检查所见
        /// </summary>
        public string ReportContent { get; set; }

        /// <summary>
        /// 报告时间
        /// </summary>
        public Nullable<System.DateTime> ReportTime { get; set; }

        /// <summary>
        /// 报告医生
        /// </summary>
        public string ReportDoctor { get; set; }

        /// <summary>
        /// 审核医生
        /// </summary>
        public string AuditDoctor { get; set; }

        /// <summary>
        /// 影像附件地址
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
