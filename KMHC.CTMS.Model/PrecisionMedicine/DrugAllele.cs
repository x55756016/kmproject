/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
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
    public class DrugAllele : BaseModel
    {
        public string ID { get; set; }
        public string GeneAlleleID { get; set; }
        public string DrugBankID { get; set; }
        public decimal EffectType { get; set; }
        public string Effect { get; set; }

    }
}
