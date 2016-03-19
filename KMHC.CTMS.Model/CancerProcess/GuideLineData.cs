/*
 * 描述:定义临床路径元数据实体
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151211      张志明              创建 
 *  
 */

using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class GuideLineData : BaseModel
    {
        /// <summary>
        /// 主键，GUID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 临床路径ID
        /// </summary>
        public string GuideLineID { get; set; }

        /// <summary>
        /// 元数据名称
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 元数据的值
        /// </summary>
        public string Value { get; set; }
    }
}
