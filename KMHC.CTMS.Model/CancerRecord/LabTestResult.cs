/*
 * 描述:检验详细结果
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151101   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class LabTestResult
    {
        public int LabResultid { get; set; }

        /// <summary>
        /// 对应的检查结果记录
        /// </summary>
        public int ExamId { get; set; }

        /// <summary>
        /// 检验指标ID
        /// </summary>
        public int TestItemId { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string ItemValue { get; set; }
        
    }
}
