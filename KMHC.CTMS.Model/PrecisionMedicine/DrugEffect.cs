/*
 * 描述:add model DrugEffect
 *  
 * 修订历史: 
 * 日期              修改人              Email                  内容
 * 20151116   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    public class DrugEffect
    {
        public string DRUGUSERID { get; set; }
        public string DRUGINTERACTION { get; set; }
        public string DRUGGENE { get; set; }
    }
}
