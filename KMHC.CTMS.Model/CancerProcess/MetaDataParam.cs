/*
 * 描述:定义元数据接受的参数
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151210      张志明              创建 
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
    public class MetaDataParam : BaseModel
    {
        /// <summary>
        /// 主键，自增
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 元数据ID
        /// </summary>
        public int MetaDataID { get; set; }

        /// <summary>
        /// 参数名
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string ParamValue { get; set; }
    }
}
