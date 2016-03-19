/*
 * 描述:
 *  
 * 修订历史: 
 * 日期               修改人              Email                   内容
 * 2015/11/10         邓柏生                                      创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class LaboratoryTestItem
    {
        /// <summary>
        /// 项目结果ID
        /// </summary>
        public string TestitemId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string Ordernumber { get; set; }
        /// <summary>
        /// 分析项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string ItemNameEN { get; set; }
        /// <summary>
        /// 检查值
        /// </summary>
        public string ItemValue { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Reslut { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Uom { get; set; }
        /// <summary>
        /// 正常参考值
        /// </summary>
        public string NormalValue { get; set; }
        /// <summary>
        /// 范围参考值
        /// </summary>
        public string ReferenceValue { get; set; }
        /// <summary>
        /// 实验室检查结果id
        /// </summary>
        public string LabresultId { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 检查手段
        /// </summary>
        public string InspectedMeans { get; set; }


        public string ItemUnitId { get; set; }



    }
}
