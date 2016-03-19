/*
 * 描述:会员所拥有的服务类型价格
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

namespace KMHC.CTMS.Model.Product
{
   public  class MemberProducts
    {
        public string MEMBERPRODUCTID { get; set; }
        public string MEMBERNAME { get; set; }
        public string MEMBERID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string PRODUCTID { get; set; }

        public string PRODUCTUNIT { get; set; }
        public string PRODUCTUNITName { get; set; }

        public Nullable<decimal> PRODUCTNUMBER { get; set; }
        public string MEMPRODESCRIPT { get; set; }
    }
}
