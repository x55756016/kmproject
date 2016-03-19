/*
 * 描述:
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2015/11/10     邓柏生                                      创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class LaboratoryResult
    {
        /// <summary>
        /// 实验室结果ID
        /// </summary>
        public string  LabresultId { get; set; }
        /// <summary>
        /// 就诊史ID
        /// </summary>
        public string  HistoryId { get; set; }
        /// <summary>
        /// 检查时间
        /// </summary>
        public Nullable<System.DateTime> DiagnosisTime { get; set; }
        /// <summary>
        /// 住院/门诊号
        /// </summary>
        public string Diagnosis { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string Hospital { get; set; }
        /// <summary>
        /// 床号
        /// </summary>
        public string DiagnosisReport { get; set; }
        /// <summary>
        /// 临床印象
        /// </summary>
        public string CI { get; set; }
        /// <summary>
        /// 检验目的
        /// </summary>
        public string MedicalhisAttachment { get; set; }
        /// <summary>
        /// 检查模板ID
        /// </summary>
        public string LabmodelId { get; set; }
        /// <summary>
        /// 检查表名
        /// </summary>
        public string LabtabelName { get; set; }
        /// <summary>
        /// 检查标准
        /// </summary>
        public string Teststandard { get; set; }
    }

    /// <summary>
    /// 实验室检查结果扩展Model
    /// </summary>
    public class LabResultResponse
    {
        public LaboratoryResult LabResult;
        public IList<LaboratoryTestItem> LabItems;
        public IList<FileUpload> FileUplpads;
    }
}
