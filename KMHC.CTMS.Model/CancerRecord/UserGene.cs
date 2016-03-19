/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class UserGene
    {
        public string ID { get; set; }
        public string GeneID { get; set; }
        public string Allele1ID { get; set; }
        public string Allele2ID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyBy { get; set; }
        public string UserID { get; set; }
        public int CopyNumber1 { get; set; }
        public int CopyNumber2 { get; set; }
        public Gene GeneModel { get; set; }
        public GeneAllele Allele1Model { get; set; }
        public GeneAllele Allele2Model { get; set; }
    }
}
