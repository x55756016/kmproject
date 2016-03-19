/*
 * 描述:基因Model
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    public class Gene:BaseModel
    {
        /// <summary>
        /// GUID主键
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 基因名
        /// </summary>
        public string GeneName { get; set; }

        /// <summary>
        /// 同义词
        /// </summary>
        public string SynonymName { get; set; }

        /// <summary>
        /// 引用序列
        /// </summary>
        public string RefSequence { get; set; }

        /// <summary>
        /// 等位基因列表
        /// </summary>
        public List<GeneAllele> GeneAlleleList { get; set; }
    }
}
