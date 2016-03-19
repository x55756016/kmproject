/*
 * 描述:基因位点Model
 *  
 * 修订历史: 
 * 日期         修改人              内容
 * 20151113     张志明              创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    public class GeneAlleleLocus:BaseModel
    {
        public string ID { get; set; }
        
        /// <summary>
        /// 对应等位基因ID
        /// </summary>
        public string GeneAlleleID { get; set; }
        
        /// <summary>
        /// 类型
        /// </summary>
        public string LocusType { get; set; }

        /// <summary>
        /// 起始位置
        /// </summary>
        public int StartPosition { get; set; }

        /// <summary>
        /// 终止位置
        /// </summary>
        public int EndPosition { get; set; }

        /// <summary>
        /// 标准值
        /// </summary>
        public string StandardValue { get; set; }

        /// <summary>
        /// 变化值
        /// </summary>
        public string VariantValue { get; set; }

        /// <summary>
        /// 变化类型
        /// </summary>
        public string VariantType { get; set; }

        /// <summary>
        /// 变异种类
        /// </summary>
        public string VariantCategory { get; set; }

        /// <summary>
        /// 变化编码
        /// </summary>
        public string VariantCode { get; set; }

        /// <summary>
        /// HGVS名称
        /// </summary>
        public string HGVSName { get; set; }

        /// <summary>
        /// 氨基酸影响
        /// </summary>
        public string AminoAcidEffect { get; set; }

        /// <summary>
        /// 变化规范
        /// </summary>
        public string VariantGuideLine { get; set; }
    }
}
