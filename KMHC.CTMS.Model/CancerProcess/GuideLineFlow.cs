/*
 * 描述:
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
using KMHC.CTMS.Model.PrecisionMedicine;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class GuideLineFlow 
    {
        /// <summary>
        /// 主键Guid
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string GUIDELINENAME { get; set; }

        /// <summary>
        /// 流程信息
        /// </summary>
        public string GUIDELINEINFO { get; set; }

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
        public virtual string OwnerID { get; set; }

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
