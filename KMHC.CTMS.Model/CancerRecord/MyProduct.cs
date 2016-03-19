/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    /// <summary>
    /// 
    /// </summary>
    public class MyProduct:BaseModel
    {
        /// <summary>
        /// ID 主键
        /// </summary>
        public string ID { get; set; }
        
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 次数
        /// </summary>
        public int ProductNum { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public Nullable<System.DateTime> StartDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public Nullable<System.DateTime> EndDate { get; set; }

        /// <summary>
        /// 是否被使用
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 使用日期
        /// </summary>
        public Nullable<System.DateTime> UsedDate { get; set; }

        public Products Product { get; set; }
    }
}
