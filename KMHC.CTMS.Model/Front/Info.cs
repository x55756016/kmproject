/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2016/3/14  邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.Model.PrecisionMedicine;
using MongoDB.Bson;

namespace KMHC.CTMS.Model.Front
{
    public class KaInfo : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ObjectId _id { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public string Cid { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
