/*
 * 描述:待办事项表Model
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2015/12/31 邓柏生                                      创建 
 *  
 */

using System;
using System.Collections.Generic;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class UserEvent
    {
        public string EventID { get; set; }
        public string UserApplyId { get; set; }
        public string ActionType { get; set; }
        public string ActionInfo { get; set; }
        public Nullable<System.DateTime> ReceiptTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string ActionStatus { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string ModelType { get; set; }
        public string ModelId { get; set; }
        public string Remarks { get; set; }
        public string LinkUrl { get; set; }
    }

    /// <summary>
    /// 扩展UserEvent实体
    /// </summary>
    public class ExtUserEvent
    {
        /// <summary>
        /// EventID
        /// </summary>
        public string EventID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string ActionStatus { get; set; } 

        /// <summary>
        ///备注 
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 医生建议
        /// </summary>
        public string DoctorSuggest { get; set; }

        /// <summary>
        /// 当前所处GuideLine
        /// </summary>
        public string CurrentNodeName { get; set; }

        /// <summary>
        /// 当前待办
        /// </summary>
        public string RecommendProcess { get; set; }

        /// <summary>
        /// 历史轨迹
        /// </summary>
        public IList<UserEvent> Tracks { get; set; }

        /// <summary>
        /// 推荐产品
        /// </summary>
        public IList<EventProduct> Products { get; set; }
    }
}
