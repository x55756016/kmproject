/*
 * 描述:定义角色功能权限
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151225      张志明              创建 
 *  
 */

using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Authorization
{
    /// <summary>
    /// 角色功能
    /// </summary>
    public class RoleFunction : BaseModel
    {
        /// <summary>
        /// 角色功能ID
        /// </summary>
        public string RoleFunctionID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// 功能ID
        /// </summary>
        public string FunctionID { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public int PermissionValue { get; set; }

        /// <summary>
        /// 取值范围
        /// </summary>
        public string DataRange { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
