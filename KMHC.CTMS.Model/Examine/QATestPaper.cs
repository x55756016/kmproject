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

namespace KMHC.CTMS.Model.Examine
{
    public class QATestPaper:BaseModel
    {
        public string TestPaperID { get; set; }
        public string TestPaperCode { get; set; }
        public string TestPaperName { get; set; }
        public string DiseaseCode { get; set; }
        public string DiseaseName { get; set; }
        public string TestPaperSource { get; set; }
        public Nullable<decimal> TotalScore { get; set; }
        public string TestPaperDescription { get; set; }
    }
}
