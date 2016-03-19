/*
 * 描述: 检验指标维护
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151101   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class LabTestItem
    {
        public int TestItemId { get; set; }
        /// <summary>
        /// 检验指标编号
        /// </summary>
        public int TestItemNo { get; set; }
        /// <summary>
        /// 检验指标名称
        /// </summary>
        public string TestItemName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 正常值,文字描述
        /// </summary>
        public string NormalValue { get; set; }
    }
}
