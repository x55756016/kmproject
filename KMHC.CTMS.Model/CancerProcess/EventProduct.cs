/*
 * 描述:
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2016/1/23      邓柏生                                      创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class EventProduct
    {
        /// <summary>
        /// 待办关联产品服务ID
        /// </summary>
        public string EventProductId { get; set; }
        /// <summary>
        /// 待办事项ID
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public Nullable<decimal> ProductPrice { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string ProductDes { get; set; }
        /// <summary>
        /// 是否已购买
        /// </summary>
        public string IsAlreadyBuy { get; set; }
    }
}
