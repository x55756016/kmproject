/*
 * 描述:定义全流程条件的元数据
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151210      张志明              创建 
 *  
 */

using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class MetaData : BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 分类(如：病人，医生等等)
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 数据类型(整数，小数，字符串)
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        ///  数据类型(整数，小数，字符串)
        /// </summary>
        public string DataTypeText { get { return EnumHelper.GetDescription(DataType); } }

        /// <summary>
        /// 数据来源的类型(表,函数,存储过程)
        /// </summary>
        public DataSourceType DataSourceType { get; set; }

        /// <summary>
        /// 数据来源的类型(表,函数,存储过程)
        /// </summary>
        public string DataSourceTypeText { get { return EnumHelper.GetDescription(DataSourceType); } }

        /// <summary>
        /// 数据来源对象
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 数据库对象的列(数据来源类型为表时填写)
        /// </summary>
        public string DataSourceColumn { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public List<MetaDataParam> Params { get; set; }

        /// <summary>
        /// 父ID,生成树形时使用
        /// </summary>
        public int ParentID { get; set; }
    }

    public class TreeItem
    {
        public string text { get; set; }

        public int value { get; set; }

        public List<TreeItem> nodes { get; set; }
    }
}
