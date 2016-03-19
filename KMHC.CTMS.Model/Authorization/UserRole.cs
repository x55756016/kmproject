/*
 * 描述:定义用户角色关系实体
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
    /// 用户角色关系
    /// </summary>
    public class UserRole : BaseModel
    {
        /// <summary>
        /// 用户角色ID
        /// </summary>
        public string UserRoleID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }
    }
}
