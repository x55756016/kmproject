/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Common
{
    public class MenuInfo:BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// IconClass
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否展开，有子菜单时有效
        /// </summary>
        public bool IsExpand { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<MenuInfo> ChildrenList { get; set; }
    }
}
