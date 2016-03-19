/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2016/2/16  邓柏生                                      创建 
 *  
 */

using System;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class HrDictionary
    {
        /// <summary>
        /// ID
        /// </summary>
        public string DictionaryId { get; set; }
        /// <summary>
        /// 父字典ID
        /// </summary>
        public string FatherId { get; set; }
        /// <summary>
        /// 字典类型编码
        /// </summary>
        public string DictionCategory { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictionaryName { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        public string DictionaryValue { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public Nullable<System.DateTime> UpdateDate { get; set; }
        /// <summary>
        /// 字典类型名
        /// </summary>
        public string DictionCategoryName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 系统类型名
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 系统类型编码
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public Nullable<decimal> OrderNumber { get; set; }
        /// <summary>
        /// 系统必须字典
        /// </summary>
        public string SystemNeed { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public Nullable<bool> IsDeleted { get; set; }
    }
}
