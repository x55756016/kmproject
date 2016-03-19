/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Common
{
    #region 用户类型信息

    /// <summary>
    /// USERINFO的UserType字段
    /// </summary>
    public enum UserType
    {
        医生=1,
        患者=2,
        管理员=3,
        医学编辑=4,
        客服 = 5
    }

    #endregion


    #region 全流程

    /// <summary>
    /// 元数据 数据类型
    /// </summary>
    public enum DataType
    {
        [Description("字符串")]
        String=0,

        [Description("整数")]
        Int=1,

        [Description("小数")]
        Double=2,

        [Description("布尔值")]
        Bool=3
    }
     
    /// <summary>
    /// 数据来源类型
    /// </summary>
    public enum DataSourceType
    {
        [Description("表")]
        Table=0,

        [Description("函数")]
        Func=1,

        [Description("存储过程")]
        StoreProcess=2
    }

    /// <summary>
    /// 条件类型
    /// </summary>
    public enum CondType 
    {
        [Description("自定义条件")]
        Custom=0,

        [Description("组合条件")]
        Combo=1
    }

    /// <summary>
    /// 运算符
    /// </summary>
    public enum Operator
    {
        [Description("等于")]
        Equal = 0,

        [Description("不等于")]
        NotEqual = 1,

        [Description("大于")]
        GreaterThen = 2,

        [Description("大于等于")]
        GreaterEqual = 3,

        [Description("小于")]
        LessThen = 4,

        [Description("小于等于")]
        LessEqual = 5,

        [Description("包含")]
        Contain = 6,

        [Description("不包含")]
        NotContain = 7
    }

    /// <summary>
    /// 逻辑运算符
    /// </summary>
    public enum LogicOperator
    {
        [Description("与")]
        And = 0,

        [Description("或")]
        Or = 1,

        [Description("非")]
        Not = 2,
    }





    /// <summary>
    /// CTMS_USEREVENT表的ActionStatus字段
    /// </summary>
    public enum ActionStatus
    {
        [Description("待办")]
        Progress=1,

        [Description("完成")]
        Complete = 3
    }


    /// <summary>
    /// CTMS_USEREVENT表的ActionType字段
    /// </summary>
    public enum ActionType
    {
        [Description("待办事项")]
        待办事项=1,

        [Description("消息通知")]
        消息通知=2
    }

    #region 客服追踪类型枚举
    public enum TraceType
    {
        [Description("电话")]
        电话 = 0,

        [Description("即时通讯")]
        即时通讯 = 1,

        [Description("其他")]
        其他 = 2
    }
    #endregion



    #endregion

    #region 权限相关

    /// <summary>
    /// 功能状态
    /// </summary>
    public enum FunctionStatus
    { 
        [Description("正常")]
        Normal=0,

        [Description("禁用")]
        Disabled=1,
 
        [Description("暂停使用")]
        Paused=2 
    }

    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        [Description("启用")]
        Normal = 0,

        [Description("禁用")]
        Disabled = 1,
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType
    {
        [Description("查看")]
        View = 1,

        [Description("查询")]
        Search = 2,

        [Description("新增")]
        Add = 4,

        [Description("修改")]
        Modify = 8,

        [Description("删除")]
        Delete = 16,

        [Description("导入")]
        Upload = 32,

        [Description("导出")]
        DownLoad = 64
    }
    
    #endregion

    #region 产品
    /// <summary>
    /// 支付类型
    /// </summary>
    public enum SpendType
    {
        [Description("充值")]
        Deposit = 0,

        [Description("购买")]
        Buy = 1,

        [Description("消费")]
        Consume = 2
    }
    #endregion
    public static class EnumHelper
    {
       public static string GetDescription(this Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null&&nameInstead == true)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }
    }
    
}
