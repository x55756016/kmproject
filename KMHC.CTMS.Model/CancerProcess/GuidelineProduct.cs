/*
 * 描述:临床路径关联产品服务
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2016/1/22      邓柏生                                      创建 
 *  
 */

using System;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class GuidelineProduct
    {
        /// <summary>
        /// 临床路径关联产品服务ID
        /// </summary>
        public string GuidelineProductId { get; set; }
        /// <summary>
        /// 临床路径ID
        /// </summary>
        public string GuidelineId { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public string ProductId { get; set; }
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
        public string Productdes { get; set; }
        /// <summary>
        /// 是否已购买
        /// </summary>
        public string IsAlreadyBuy { get; set; }
    }
}
