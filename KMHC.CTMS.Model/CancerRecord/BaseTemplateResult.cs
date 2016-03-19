/*
 * 描述:依据模板检验结果
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 *20160115  		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class BaseTemplateResult
    {

        public string Name { get; set; }


        public string BASETEMPLATEID { get; set; }
        public string HISTORYID { get; set; }
        public string EXAMINETEMPLATESID { get; set; }
        public string RESULT { get; set; }
        public string CREATEUSERID { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public string EDITUSERID { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public string ISDELETED { get; set; }
        public string OWNERID { get; set; }
    }
}
