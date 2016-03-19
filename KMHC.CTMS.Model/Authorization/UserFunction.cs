/*
 * 描述:用户所拥有的菜单权限
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 * 20160229   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Authorization
{
    /// <summary>
    /// 用户所拥有的菜单权限
    /// </summary>
    public class UserFunction
    {
        public string ROLEID { get; set; }
        public string FUNCTIONID { get; set; }
        public int PERMISSIONVALUE { get; set; }
        public string PERMISSIONCODE { get; set; }
        public string FUNCTIONCODE { get; set; }
    }
}
