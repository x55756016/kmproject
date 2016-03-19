/*
 * 描述:中国药典Model
 *  
 * 修订历史: 
 * 日期                    修改人              Email                   内容
 * 2015/11/13 16:11:43     邓柏生                                      创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    public class CnDrug:BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string DrugBankID { get; set; }

        /// <summary>
        /// 药名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string PinyinCode { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 同义词（别名）
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 种类
        /// </summary>
        public string KindName { get; set; }

        /// <summary>
        /// 是否处方药
        /// </summary>
        public Nullable<bool> IsPrescription { get; set; }

        /// <summary>
        /// 是否医保用药
        /// </summary>
        public Nullable<bool> IsMedicalInsurance { get; set; }

        /// <summary>
        /// 生产厂商
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Pack { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        public string DosageForms { get; set; }

        /// <summary>
        /// 用法用量
        /// </summary>
        public string Dosage { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 主治功能
        /// </summary>
        public string Indication { get; set; }

        /// <summary>
        /// 明细
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 禁忌症
        /// </summary>
        public string Contraindication { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 不良反应
        /// </summary>
        public string Adverse { get; set; }
    }
}
