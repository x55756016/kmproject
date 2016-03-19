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
    public class MyQuestion : BaseModel
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string LoginName { get; set; }
        public string ObjectType { get; set; }
        public string ObjectUserID { get; set; }
        public string ObjectLoginName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
