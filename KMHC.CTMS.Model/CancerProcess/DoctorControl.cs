/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                  内容
 *    		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.PrecisionMedicine;

namespace KMHC.CTMS.Model.CancerProcess
{
    public partial class UserApply
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public string APPLYID { get; set; }
        public string USERID { get; set; }
        public string GUIDELINEID { get; set; }
        public string CURRENTNODE { get; set; }

        public string CurrentName { get; set; }

        public string NextCurrentNode { get; set; }

        public string STATUS { get; set; }
        public string DOCTORSUGGEST { get; set; }
        public Nullable<System.DateTime> ENTRYDATE { get; set; }
        public Nullable<System.DateTime> EXITDATE { get; set; }
        public string CREATEUSERID { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public string EDITUSERID { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public Nullable<bool> ISDELETED { get; set; }
        public string OWNERID { get; set; }

    }

    public partial class UserApply
    {

        public CnrUser CnrUser { get; set; }


        public string EventId { get; set; }

        public string NextCurrentNodeName { get; set; }


        public string DiseaseInfoId { get; set; }


    }


}
