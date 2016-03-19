/*
 * 描述:
 *  
 * 修订历史: 
 * 日期          修改人              Email                   内容
 * 2016/1/5      邓柏生                                      创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class Symptom
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 症状名称
        /// </summary>
        public string SymptomName { get; set; }
        /// <summary>
        /// 症状名称字典ID
        /// </summary>
        public string DictsymptomId { get; set; }
        /// <summary>
        /// 记录创建时间
        /// </summary>
        public Nullable<System.DateTime> CreateDate { get; set; }
        /// <summary>
        /// 记录更新时间
        /// </summary>
        public Nullable<System.DateTime> EditDate { get; set; }
    }

    public class SymptomExt
    {
        /// <summary>
        /// SymptomID
        /// </summary>
        public string SymptomID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 症状名称字典ID
        /// </summary>
        public string DictsymptomId { get; set; }
        /// <summary>
        /// 症状名称
        /// </summary>
        public string SymptomName { get; set; }
        /// <summary>
        /// 用户症状等级
        /// </summary>
        public string SymptomLevel { get; set; }
        /// <summary>
        /// 症状时间
        /// </summary>
        public Nullable<System.DateTime> SymptomDate { get; set; }
    }
}
