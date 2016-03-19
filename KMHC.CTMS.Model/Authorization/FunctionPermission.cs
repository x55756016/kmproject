/*
 * 描述:定义功能权限关系
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
    /// 功能权限关系实体(某个功能的增删改等)
    /// </summary>
    public class FunctionPermission:BaseModel
    {
        /// <summary>
        /// 功能权限ID
        /// </summary>
        public string FunctionPermissionID { get; set; }

        /// <summary>
        /// 功能ID
        /// </summary>
        public string FunctionID { get; set; }

        /// <summary>
        /// 权限类型ID
        /// </summary>
        public string PermissionID { get; set; }

        /// <summary>
        /// 权限类型的值
        /// </summary>
        public int PermissionValue { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 行动名
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
