/*
 * 描述:地区
 *  
 * 修订历史: 
 * 日期              修改人              Email                  内容
 * 20160122   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Common
{
    public class Area
    {
        public int AREAID { get; set; }
        public string AREANAME { get; set; }
        public int PARENTID { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public string CREATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public string UPDATEBY { get; set; }
        public Nullable<byte> STATUS { get; set; }
    }
}
