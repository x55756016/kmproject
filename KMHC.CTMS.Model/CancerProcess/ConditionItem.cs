/*
 * 描述:定义全流程的条件项
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151210      张志明              创建 
 *  
 */

using KMHC.CTMS.Common;
using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class ConditionItem : BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 条件类型(单一条件,组合条件)
        /// </summary>
        public CondType CondType { get; set; }

        public string CondTypeText { get { return EnumHelper.GetDescription(CondType); } }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 数据类型(整数，小数，字符串)
        /// </summary>
        public DataType DataType { get; set; }

        public string DataTypeText { get { return EnumHelper.GetDescription(DataType); } }
        /// <summary>
        /// 分类(用户选择分类)
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 元数据ID(单一条件时指定)
        /// </summary>
        public Nullable<int> MetaDataID { get; set; }

        /// <summary>
        /// 元数据名称
        /// </summary>
        public string MetaDataName{ get; set; }

        /// <summary>
        /// 运算符(=,!=，>,>=,<,<=,包含,不包含)
        /// </summary>
        public Operator Operator { get; set; }

        public string OperatorText { get { return EnumHelper.GetDescription(Operator); } }

        /// <summary>
        /// 比较的值
        /// </summary>
        public string OperateValue { get; set; }

        /// <summary>
        /// 逻辑运算符(组合条件时指定)
        /// </summary>
        public LogicOperator LogicalOperator { get; set; }

        public string LogicalOperatorText { get { return EnumHelper.GetDescription(LogicalOperator); } }

        /// <summary>
        /// 条件列表(组合条件时选定)
        /// </summary>
        public List<ConditionItem> ComboCondItemList { get; set; }

        /// <summary>
        /// 条件名称
        /// </summary>
        public string ComboCondItemNames { get; set; }
    }
}
