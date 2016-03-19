/*
 * 描述:定义所有model的基类
 *  
 * 修订历史: 
 * 日期         修改人              内容
 * 20151113     张志明              创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    /// <summary>
    /// 所有模型的基类
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        public virtual string CreateUserID { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public virtual string CreateUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateDateTime { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public virtual string EditUserID { get; set; }


        /// <summary>
        /// 修改人姓名
        /// </summary>
        public virtual string EditUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? EditTime { get; set; }

        /// <summary>
        /// 所属人ID
        /// </summary>
        public virtual string OwnerID {get; set;}

        /// <summary>
        /// 所属人姓名
        /// </summary>
        public virtual string OwnerName { get; set; }

        /// <summary>
        /// 是否被删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }

    }
}
