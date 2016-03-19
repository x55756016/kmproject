/*
 * 描述:等位基因Model
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151113      张志明              创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    public class GeneAllele:BaseModel
    {
        public string ID { get; set; }

        /// <summary>
        /// 等位基因名
        /// </summary>
        public string GeneAlleleName { get; set; }
        
        /// <summary>
        /// 对应基因表ID
        /// </summary>
        public string GeneID { get; set; }

        /// <summary>
        /// 同义词
        /// </summary>
        public string SynonymName { get; set; }
        
        /// <summary>
        /// 标准名
        /// </summary>
        public string StandardName { get; set; }

        /// <summary>
        /// 功能描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 对应Gene
        /// </summary>
        public Gene Gene { get; set; }

        /// <summary>
        /// 等位基因位点列表
        /// </summary>
        public List<GeneAlleleLocus> GeneAlleleLocusList { get; set; }

        /// <summary>
        /// 蛋白
        /// </summary>
        public string Protein { get; set; }

        /// <summary>
        /// 酶活性
        /// </summary>
        public string EnzymaticActivity { get; set; }
    }
}
