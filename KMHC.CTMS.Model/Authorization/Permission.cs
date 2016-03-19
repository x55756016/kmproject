﻿/*
 * 描述:定义权限类型实体
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
    /// 权限类型实体
    /// </summary>
    public class Permission : BaseModel
    {
        /// <summary>
        /// 权限类型ID
        /// </summary>
        public string PermissionID { get; set; }

        /// <summary>
        /// 权限类型名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 权限类型值(用2的n次方表示)
        /// </summary>
        public int PermissionValue { get; set; }

        /// <summary>
        /// 权限类型的Code
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
